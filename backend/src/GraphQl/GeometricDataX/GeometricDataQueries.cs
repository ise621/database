using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Database.Data;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using Guid = System.Guid;

namespace Database.GraphQl.GeometricDataX;

[ExtendObjectType(nameof(Query))]
public sealed class GeometricDataQueries
{
    [UsePaging]
    [UseFiltering]
    [UseSorting]
    public IQueryable<GeometricData> GetAllGeometricData(
        DateTime? timestamp,
        [GraphQLType<LocaleType>] string? locale,
        ApplicationDbContext context
    )
    {
        return context.GeometricData;
    }

    public Task<GeometricData?> GetGeometricDataAsync(
        Guid id,
        DateTime? timestamp,
        [GraphQLType<LocaleType>] string? locale,
        GeometricDataByIdDataLoader byId,
        CancellationToken cancellationToken
    )
    {
        return byId.LoadAsync(
            id,
            cancellationToken
        );
    }
}