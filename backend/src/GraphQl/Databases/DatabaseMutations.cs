using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Database.Metabase;
using GraphQL;
using HotChocolate;
using HotChocolate.Types;
using Microsoft.AspNetCore.Http;

namespace Database.GraphQl.Databases;

[ExtendObjectType(nameof(Mutation))]
public sealed class DatabaseMutations
{
    private static readonly string[] _updateDatabaseFileNames =
    {
        "UpdateDatabase.graphql"
    };

    public async Task<UpdateDatabasePayload> UpdateDatabaseAsync(
        UpdateDatabaseInput input,
        AppSettings appSettings,
        IHttpClientFactory httpClientFactory,
        IHttpContextAccessor httpContextAccessor,
        CancellationToken cancellationToken
    )
    {
        return (await QueryingMetabase.QueryGraphQl<UpdateDatabasePayload>(
                   appSettings,
                   new GraphQLRequest(
                       await QueryingMetabase.ConstructGraphQlQuery(
                           _updateDatabaseFileNames
                       ).ConfigureAwait(false),
                       new
                       {
                           input
                       },
                       "UpdateDatabase"
                   ),
                   httpClientFactory,
                   httpContextAccessor,
                   cancellationToken
               ).ConfigureAwait(false))?.Data
               ?? new UpdateDatabasePayload(
                   null,
                   new[]
                   {
                       new UpdateDatabaseError(
                           UpdateDatabaseErrorCode.UNKNOWN,
                           "Unknown error.",
                           Array.Empty<string>()
                       )
                   }
               );
    }
}