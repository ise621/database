using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Data.Sorting;
using HotChocolate.Types;
using Database.Data;
using Database.GraphQl.Extensions;

namespace Database.GraphQl.GeometricDataX;

[ExtendObjectType(nameof(Query))]
public sealed class GeometricDataQueries
{
    [UsePaging]
    [UseFiltering]
    [UseSorting]
    public IQueryable<GeometricData> GetAllGeometricData(
        [GraphQLType<LocaleType>] string? locale,
        ApplicationDbContext context,
        ISortingContext sorting
    )
    {
        sorting.StabilizeOrder<GeometricData>();
        // TODO Use `locale`.
        return context.GeometricData.AsNoTracking();
    }

    public Task<GeometricData?> GetGeometricDataAsync(
        Guid id,
        [GraphQLType<LocaleType>] string? locale,
        GeometricDataByIdDataLoader byId,
        CancellationToken cancellationToken
    )
    {
        // TODO Use `locale`.
        return byId.LoadAsync(
            id,
            cancellationToken
        );
    }
}