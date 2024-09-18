namespace Database.GraphQl.Databases;

public sealed class DatabaseEdge
{
    public DatabaseEdge(
        Database node
    )
    {
        Node = node;
    }

    public Database Node { get; }
}