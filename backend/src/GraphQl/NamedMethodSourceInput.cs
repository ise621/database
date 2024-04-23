namespace Database.GraphQl;

public sealed record NamedMethodSourceInput(
    string Name,
    CrossDatabaseDataReferenceInput Value
);