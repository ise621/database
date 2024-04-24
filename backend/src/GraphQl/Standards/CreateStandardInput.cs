using System;
using Database.Enumerations;
using Database.GraphQl.Numerations;

namespace Database.GraphQl.Standards;

public sealed record CreateStandardInput(
    string? Title,
    string? Abstract,
    string? Section,
    int? Year,
    CreateNumerationInput Numeration,
    Standardizer[] Standardizers,
    Uri? Locator
);