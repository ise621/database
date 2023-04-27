using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Types;
using Microsoft.AspNetCore.Http;

namespace Database.GraphQl.Databases
{
    [ExtendObjectType(nameof(Mutation))]
    public sealed class DatabaseMutations
    {
        public async Task<UpdateDatabasePayload> UpdateDatabaseAsync(
            UpdateDatabaseInput input,
            [Service] AppSettings appSettings,
            [Service] IHttpClientFactory httpClientFactory,
            [Service] IHttpContextAccessor httpContextAccessor,
            CancellationToken cancellationToken
            )
        {
            return (await QueryingMetabase.QueryMetabase<UpdateDatabasePayload>(
                appSettings,
                new GraphQL.GraphQLRequest(
                    query: await QueryingMetabase.ConstructQuery(
                        new[] {
                            "UpdateDatabase.graphql"
                        }
                    ).ConfigureAwait(false),
                    variables: new
                    {
                        input,
                    },
                    operationName: "UpdateDatabase"
                ),
                httpClientFactory,
                httpContextAccessor,
                cancellationToken
            ).ConfigureAwait(false))?.Data
            ?? new UpdateDatabasePayload(
                null,
                new []
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
}