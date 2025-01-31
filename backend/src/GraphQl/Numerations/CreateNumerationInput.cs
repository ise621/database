namespace Database.GraphQl.Numerations;

public sealed record CreateNumerationInput(
    string? Prefix,
    string MainNumber,
    string? Suffix
);