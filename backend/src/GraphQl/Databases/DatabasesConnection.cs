using System.Collections.Generic;

namespace Database.GraphQl.Databases;

public sealed class DatabasesConnection
{
    // pageInfo: PageInfo!
    // totalCount: Int!

    public DatabasesConnection(
        IReadOnlyList<Database>? nodes
    )
    {
        Nodes = nodes;
    }

    // edges: [DatabasesEdge!]
    public IReadOnlyList<Database>? Nodes { get; }
}