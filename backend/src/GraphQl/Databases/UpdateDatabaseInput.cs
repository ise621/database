using System;

namespace Database.GraphQl.Databases
{
    public record UpdateDatabaseInput(
          Guid DatabaseId,
          string Name,
          string Description,
          Uri Locator
        );
}