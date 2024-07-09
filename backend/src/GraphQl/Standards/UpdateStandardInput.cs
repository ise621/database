using System;
using Database.Enumerations;
using Database.GraphQl.Numerations;

namespace Database.GraphQl.Standards;

public sealed record UpdateStandardInput(
    string? Title,
    string? Abstract,
    string? Section,
    int? Year,
    UpdateNumerationInput Numeration,
    Standardizer[] Standardizers,
    Uri? Locator
);