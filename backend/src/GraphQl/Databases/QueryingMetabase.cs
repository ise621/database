using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using GraphQL.Client.Serializer.SystemTextJson;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using OpenIddict.Client.AspNetCore;

namespace Database.GraphQl.Databases
{
    public sealed class QueryingMetabase
    {
        private static readonly JsonSerializerOptions SerializerOptions =
            new()
            {
                Converters = { new JsonStringEnumConverter(new ConstantCaseJsonNamingPolicy(), false), },
                NumberHandling = JsonNumberHandling.Strict,
                PropertyNameCaseInsensitive = false,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                ReadCommentHandling = JsonCommentHandling.Disallow,
                IncludeFields = false,
                IgnoreReadOnlyProperties = false,
                IgnoreReadOnlyFields = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            }; //.SetupImmutableConverter();

        public static async Task<string> ConstructQuery(
            string[] fileNames
        )
        {
            return string.Join(
                Environment.NewLine,
                await Task.WhenAll(
                    fileNames.Select(fileName =>
                        File.ReadAllTextAsync($"GraphQl/Databases/Queries/{fileName}")
                    )
                ).ConfigureAwait(false)
            );
        }

        public static async
          Task<GraphQL.GraphQLResponse<TGraphQlResponse>>
          QueryMetabase<TGraphQlResponse>(
            AppSettings appSettings,
            GraphQL.GraphQLRequest request,
            IHttpClientFactory httpClientFactory,
            IHttpContextAccessor httpContextAccessor,
            CancellationToken cancellationToken
            )
          where TGraphQlResponse : class
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
            // For some reason `httpClient.PostAsJsonAsync` without `MakeJsonHttpContent` but with `SerializerOptions` results in `BadRequest` status code. It has to do with `JsonContent.Create` used within `PostAsJsonAsync` --- we also cannot use `JsonContent.Create` in `MakeJsonHttpContent`. What is happening here?
            using var jsonHttpContent = MakeJsonHttpContent(request);
            using var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Post,
                // TODO Consider using [Flurl](https://flurl.dev) to construct URIs. For the pitfalls of using `Uri` as below see the comments to https://stackoverflow.com/questions/372865/path-combine-for-urls/1527643#1527643
                new Uri(new Uri(appSettings.MetabaseHost), "/graphql/")
                );
            httpRequestMessage.Content = jsonHttpContent;
            httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
            using var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage, cancellationToken).ConfigureAwait(false);
            if (httpResponseMessage.StatusCode != HttpStatusCode.OK)
            {
                throw new HttpRequestException($"The status code is not {HttpStatusCode.OK}.", null, httpResponseMessage.StatusCode);
            }
            // We could use `httpResponseMessage.Content.ReadFromJsonAsync<GraphQL.GraphQLResponse<TGraphQlResponse>>` which would make debugging more difficult though, https://docs.microsoft.com/en-us/dotnet/api/system.net.http.json.httpcontentjsonextensions.readfromjsonasync?view=net-5.0#System_Net_Http_Json_HttpContentJsonExtensions_ReadFromJsonAsync__1_System_Net_Http_HttpContent_System_Text_Json_JsonSerializerOptions_System_Threading_CancellationToken_
            using var graphQlResponseStream =
                await httpResponseMessage.Content
                .ReadAsStreamAsync(cancellationToken)
                .ConfigureAwait(false);
            // Console.WriteLine("aaaaaaaaaaaaaa");
            // Console.WriteLine(new StreamReader(graphQlResponseStream).ReadToEnd());
            // {"data":{"databases":{"nodes":[{"uuid":"70fe99ed-f212-4666-adde-6e0877f3e518","name":"A","description":"B","locator":"https://local.solarbuildingenvelopes.com:5051/graphql","verificationState":"VERIFIED","verificationCode":"","canCurrentUserUpdateNode":false,"canCurrentUserVerifyNode":false},{"uuid":"6313a485-3767-4352-b75c-2a8e431cc4a9","name":"X","description":"Y","locator":"https://local.solarbuildingenvelopes.com:5051/graphql","verificationState":"VERIFIED","verificationCode":"njckM853NWOJnH\u002BXyDXIQMwEdHteu9JIlQVgGp97ewZOTYzx3sVip3HcTRS9QfWdBUPnYAeKZg76BbTMEUwMZQ==","canCurrentUserUpdateNode":false,"canCurrentUserVerifyNode":false}]}}}
            var deserializedGraphQlResponse =
                await JsonSerializer.DeserializeAsync<GraphQL.GraphQLResponse<TGraphQlResponse>>(
                    graphQlResponseStream,
                    SerializerOptions,
                    cancellationToken
                ).ConfigureAwait(false);
            if (deserializedGraphQlResponse is null)
            {
                throw new JsonException("Failed to deserialize the GraphQL response.");
            }
            return deserializedGraphQlResponse;
        }

        private static HttpContent MakeJsonHttpContent<TContent>(
            TContent content
            )
        {
            // For some reason using `JsonContent.Create<TContent>(content, null, SerializerOptions)` results in status code `BadRequest`.
            var result =
              new ByteArrayContent(
                JsonSerializer.SerializeToUtf8Bytes(
                  content,
                  SerializerOptions
                  )
                );
            result.Headers.ContentType =
              new MediaTypeHeaderValue("application/json");
            return result;
        }
    }
}