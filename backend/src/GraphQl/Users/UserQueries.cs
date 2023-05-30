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
            ClaimsPrincipal claimsPrincipal,
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
                return await Metabase.QueryingMetabase.QueryRest<UserInfo>(
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
                    .SetCode("JSON_DESERIALIZATION_FAILED")
                    .SetPath(resolverContext.Path.ToList().Concat(e.Path?.Split('.') ?? Array.Empty<string>()).ToList()) // TODO Splitting the path at '.' is wrong in general.
                    .SetMessage($"Failed to deserialize GraphQL response of request to {uri}. The details given are: Zero-based number of bytes read within the current line before the exception are {e.BytePositionInLine}, zero-based number of lines read before the exception are {e.LineNumber}, message that describes the current exception is '{e.Message}', path within the JSON where the exception was encountered is {e.Path}.")
                    .SetException(e)
                    .Build()
                );
                return null;
            }
        }
    }
}