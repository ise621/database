namespace Database.GraphQl
{
    public record NamedMethodSourceInput(
            string Name,
            CrossDatabaseDataReferenceInput Value
    );
}