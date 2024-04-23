using System.Text.Json;

namespace Database.GraphQl;

public sealed record NamedMethodArgumentInput(
    string Name,
    JsonElement Value
);