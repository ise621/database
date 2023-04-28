using System;

namespace Database.GraphQl.Databases
{
    public sealed record UpdateDatabaseInput(
          Guid DatabaseId,
          string Name,
          string Description,
          Uri Locator
        );
}