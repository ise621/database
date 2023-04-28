using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using GraphQL.Client.Serializer.SystemTextJson;
using HotChocolate;
using HotChocolate.Resolvers;
using HotChocolate.Types;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OpenIddict.Abstractions;
using OpenIddict.Client.AspNetCore;
using Yoh.Text.Json.NamingPolicies;

namespace Database.GraphQl.Users
{
    [ExtendObjectType(nameof(Query))]
    public sealed class UserQueries
    {
        public async Task<Data.User?> GetCurrentUserAsync(
            [GlobalState(nameof(ClaimsPrincipal))] ClaimsPrincipal claimsPrincipal,
            Data.ApplicationDbContext context,
            CancellationToken cancellationToken
        )
        {
            if (!claimsPrincipal.HasClaim(ClaimTypes.NameIdentifier))
            {
                return null;
            }
            return
                await context.Users.AsQueryable()
                .SingleOrDefaultAsync(
                    u => u.Subject == claimsPrincipal.GetClaim(ClaimTypes.NameIdentifier),
                    cancellationToken
                ).ConfigureAwait(false);
        }

        public async Task<UserInfo?> GetCurrentUserInfoAsync(
            [Service] AppSettings appSettings,
            [Service] IHttpClientFactory httpClientFactory,
            [Service] IHttpContextAccessor httpContextAccessor,
            IResolverContext resolverContext,
            CancellationToken cancellationToken
        )
        {
            var uri = new Uri(new Uri(appSettings.MetabaseHost), "/connect/userinfo");
            try
            {
                return await Query<UserInfo>(
                    uri,
                    httpClientFactory,
                    httpContextAccessor,
                    cancellationToken
                );
            }
            catch (HttpRequestException e)
            {
                resolverContext.ReportError(
                    ErrorBuilder.New()
                    .SetCode("METABASE_REQUEST_FAILED")
                    .SetPath(resolverContext.Path)
                    .SetMessage($"Failed with status code {e.StatusCode} to request {uri}.")
                    .SetException(e)
                    .Build()
                );
                return null;
            }
            catch (JsonException e)
            {
                resolverContext.ReportError(
                    ErrorBuilder.New()
                    .SetCode("DESERIALIZATION_FAILED")
                    .SetPath(resolverContext.Path.ToList().Concat(e.Path?.Split('.') ?? Array.Empty<string>()).ToList()) // TODO Splitting the path at '.' is wrong in general.
                    .SetMessage($"Failed to deserialize GraphQL response of request to {uri}. The details given are: Zero-based number of bytes read within the current line before the exception are {e.BytePositionInLine}, zero-based number of lines read before the exception are {e.LineNumber}, message that describes the current exception is '{e.Message}', path within the JSON where the exception was encountered is {e.Path}.")
                    .SetException(e)
                    .Build()
                );
                return null;
            }
        }

        private static readonly JsonSerializerOptions SerializerOptions =
            new()
            {
                Converters = { new JsonStringEnumConverter(new ConstantCaseJsonNamingPolicy(), false), },
                NumberHandling = JsonNumberHandling.Strict,
                PropertyNameCaseInsensitive = false,
                // TODO When we run .NET 8, remove [Yoh.Text.Json.NamingPolicies](https://github.com/YohDeadfall/Yoh.Text.Json.NamingPolicies) and use [.NET's inbuilt snake-case support](https://github.com/dotnet/runtime/pull/69613).
                PropertyNamingPolicy = JsonNamingPolicies.SnakeCaseLower,
                ReadCommentHandling = JsonCommentHandling.Disallow,
                IncludeFields = false,
                IgnoreReadOnlyProperties = false,
                IgnoreReadOnlyFields = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            }; //.SetupImmutableConverter();

        private static async Task<TResponse> Query<TResponse>(
            [Service] Uri uri,
            [Service] IHttpClientFactory httpClientFactory,
            [Service] IHttpContextAccessor httpContextAccessor,
            CancellationToken cancellationToken
        )
            where TResponse : class
        {
            using var httpClient = httpClientFactory.CreateClient();
            string? bearerToken = null;
            if (httpContextAccessor.HttpContext is not null)
            {
                bearerToken = await httpContextAccessor.HttpContext.GetTokenAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    OpenIddictClientAspNetCoreConstants.Tokens.BackchannelAccessToken
                ).ConfigureAwait(false);
            }
            using var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                // TODO Consider using [Flurl](https://flurl.dev) to construct URIs. For the pitfalls of using `Uri` as below see the comments to https://stackoverflow.com/questions/372865/path-combine-for-urls/1527643#1527643
                uri
                );
            httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
            using var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage, cancellationToken).ConfigureAwait(false);
            if (httpResponseMessage.StatusCode != HttpStatusCode.OK)
            {
                throw new HttpRequestException($"The status code is not {HttpStatusCode.OK} but {httpResponseMessage.StatusCode}.", null, httpResponseMessage.StatusCode);
            }
            // We could use `httpResponseMessage.Content.ReadFromJsonAsync<GraphQL.GraphQLResponse<TResponse>>` which would make debugging more difficult though, https://docs.microsoft.com/en-us/dotnet/api/system.net.http.json.httpcontentjsonextensions.readfromjsonasync?view=net-5.0#System_Net_Http_Json_HttpContentJsonExtensions_ReadFromJsonAsync__1_System_Net_Http_HttpContent_System_Text_Json_JsonSerializerOptions_System_Threading_CancellationToken_
            using var responseStream =
                await httpResponseMessage.Content
                .ReadAsStreamAsync(cancellationToken)
                .ConfigureAwait(false);
            // Console.WriteLine(new StreamReader(responseStream).ReadToEnd());
            var deserializedResponse =
                await JsonSerializer.DeserializeAsync<TResponse>(
                    responseStream,
                    SerializerOptions,
                    cancellationToken
                ).ConfigureAwait(false);
            if (deserializedResponse is null)
            {
                throw new JsonException("Failed to deserialize the GraphQL response.");
            }
            return deserializedResponse;
        }
    }
}