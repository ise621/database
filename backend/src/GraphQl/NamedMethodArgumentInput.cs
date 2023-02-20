using System.Text.Json;

namespace Database.GraphQl
{
    public record NamedMethodArgumentInput(
        string Name,
        JsonElement Value
    );
}