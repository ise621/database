using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Execution;
using HotChocolate.Types;
using Microsoft.AspNetCore.Http;

namespace Database.GraphQl.Databases
{
    [ExtendObjectType(nameof(Query))]
    public sealed class DatabaseQueries
    {
        private sealed class DatabasesData
        {
            public DatabasesConnection Databases { get; set; } = default!;
        }

        public async Task<Database> GetDatabaseAsync(
            [Service] AppSettings appSettings,
            [Service] IHttpClientFactory httpClientFactory,
            [Service] IHttpContextAccessor httpContextAccessor,
            CancellationToken cancellationToken
        )
        {
            var response =
                await QueryingMetabase.QueryMetabase<DatabasesData>(
                    appSettings,
                    new GraphQL.GraphQLRequest(
                        query: await QueryingMetabase.ConstructQuery(
                            new[] {
                                "Databases.graphql"
                            }
                        ).ConfigureAwait(false),
                        variables: new
                        {
                            where = new
                            {
                                locator = new
                                {
                                    // TODO This is error-prone.
                                    eq = new Uri(new Uri(appSettings.Host), "/graphql")
                                }
                            }
                        },
                        operationName: "Databases"
                    ),
                    httpClientFactory,
                    httpContextAccessor,
                    cancellationToken
                ).ConfigureAwait(false);
            if (response is null)
            {
                throw new QueryException(
                    ErrorBuilder.New()
                    .SetCode("NULL_RESPONSE")
                    .SetMessage("Response is null.")
                    .Build()
                );
            }
            if (response.Data.Databases.Nodes is null)
            {
                throw new QueryException(
                    ErrorBuilder.New()
                    .SetCode("NULL_NODES")
                    .SetMessage("The supposed list of databases is null.")
                    .Build()
                );
            }
            if (response.Data.Databases.Nodes.Count == 0)
            {
                throw new QueryException(
                    ErrorBuilder.New()
                    .SetCode("NO_DATABASE")
                    .SetMessage("The list of databases is empty.")
                    .Build()
                );
            }
            if (response.Data.Databases.Nodes.Count >= 2)
            {
                throw new QueryException(
                    ErrorBuilder.New()
                    .SetCode("AMBIGUOUS_DATABASE")
                    .SetMessage("The list of databases has more than one entry.")
                    .Build()
                );
            }
            return response.Data.Databases.Nodes[0];
        }
    }
}
