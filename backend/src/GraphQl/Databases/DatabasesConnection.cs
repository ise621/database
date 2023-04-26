using System.Collections.Generic;

namespace Database.GraphQl.Databases
{
    public sealed class DatabasesConnection
    {
        // edges: [DatabasesEdge!]
        public IReadOnlyList<Database>? Nodes { get; }
        // pageInfo: PageInfo!
        // totalCount: Int!

        public DatabasesConnection(
            IReadOnlyList<Database>? nodes
        )
        {
            Nodes = nodes;
        }
    }
}