using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Database.Metabase;
using GraphQL;
using HotChocolate;
using HotChocolate.Execution;
using HotChocolate.Resolvers;
using HotChocolate.Types;
using Microsoft.AspNetCore.Http;

namespace Database.GraphQl.Databases;

[ExtendObjectType(nameof(Query))]
public sealed class DatabaseQueries
{
    private static readonly string[] _databaseFileNames =
    {
        "Databases.graphql"
    };

    public async Task<Database> GetDatabaseAsync(
        [Service] AppSettings appSettings,
        [Service] IHttpClientFactory httpClientFactory,
        [Service] IHttpContextAccessor httpContextAccessor,
        IResolverContext resolverContext,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var response =
                await QueryingMetabase.QueryGraphQl<DatabasesData>(
                    appSettings,
                    new GraphQLRequest(
                        await QueryingMetabase.ConstructGraphQlQuery(
                            _databaseFileNames
                        ).ConfigureAwait(false),
                        new
                        {
                            where = new
                            {
                                locator = new
                                {
                                    // TODO This is error-prone.
                                    eq = new Uri(new Uri(appSettings.Host), "/graphql/")
                                }
                            }
                        },
                        "Databases"
                    ),
                    httpClientFactory,
                    httpContextAccessor,
                    cancellationToken
                ).ConfigureAwait(false);
            if (response is null)
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetCode("NULL_RESPONSE")
                        .SetPath(resolverContext.Path)
                        .SetMessage("Response is null.")
                        .Build()
                );
            if (response.Data.Databases.Nodes is null)
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetCode("NULL_NODES")
                        .SetPath(resolverContext.Path)
                        .SetMessage("The supposed list of databases is null.")
                        .Build()
                );
            if (response.Data.Databases.Nodes.Count == 0)
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetCode("NO_DATABASE")
                        .SetPath(resolverContext.Path)
                        .SetMessage("The list of databases is empty.")
                        .Build()
                );
            if (response.Data.Databases.Nodes.Count >= 2)
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetCode("AMBIGUOUS_DATABASE")
                        .SetPath(resolverContext.Path)
                        .SetMessage("The list of databases has more than one entry.")
                        .Build()
                );
            return response.Data.Databases.Nodes[0];
        }
        catch (HttpRequestException e)
        {
            throw new QueryException(
                ErrorBuilder.New()
                    .SetCode("METABASE_REQUEST_FAILED")
                    .SetPath(resolverContext.Path)
                    .SetMessage($"Failed with status code {e.StatusCode} to request the metabase GraphQl endpoint.")
                    .SetException(e)
                    .Build()
            );
        }
        catch (JsonException e)
        {
            throw new QueryException(
                ErrorBuilder.New()
                    .SetCode("JSON_DESERIALIZATION_FAILED")
                    .SetPath(resolverContext.Path.ToList().Concat(e.Path?.Split('.') ?? Array.Empty<string>())
                        .ToList()) // TODO Splitting the path at '.' is wrong in general.
                    .SetMessage(
                        $"Failed to deserialize GraphQL response of request to the metabase GraphQl endpoint. The details given are: Zero-based number of bytes read within the current line before the exception are {e.BytePositionInLine}, zero-based number of lines read before the exception are {e.LineNumber}, message that describes the current exception is '{e.Message}', path within the JSON where the exception was encountered is {e.Path}.")
                    .SetException(e)
                    .Build()
            );
        }
    }

    private sealed class DatabasesData
    {
        public DatabasesConnection Databases { get; } = default!;
    }
}