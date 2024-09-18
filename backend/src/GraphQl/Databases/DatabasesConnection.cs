using System.Collections.Generic;

namespace Database.GraphQl.Databases;

public sealed class DatabasesConnection
{
    public DatabasesConnection(
        IReadOnlyList<DatabaseEdge>? edges
    )
    {
        Edges = edges;
    }

    public IReadOnlyList<DatabaseEdge>? Edges { get; }
}