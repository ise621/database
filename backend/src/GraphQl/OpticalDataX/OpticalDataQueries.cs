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

namespace Database.GraphQl.OpticalDataX;

[ExtendObjectType(nameof(Query))]
public sealed class OpticalDataQueries
{
    [UsePaging]
    // [UseProjection] // We disabled projections because when requesting `id` all results had the same `id` and when also requesting `uuid`, the latter was always the empty UUID `000...`.
    [UseFiltering]
    [UseSorting]
    public IQueryable<OpticalData> GetAllOpticalData(
        DateTime? timestamp,
        [GraphQLType<LocaleType>] string? locale,
        ApplicationDbContext context,
        ISortingContext sorting
    )
    {
        sorting.StabilizeOrder<OpticalData>();
        // TODO Use `timestamp` and `locale`.
        return context.OpticalData.AsNoTracking();
    }

    public Task<OpticalData?> GetOpticalDataAsync(
        Guid id,
        DateTime? timestamp,
        [GraphQLType<LocaleType>] string? locale,
        OpticalDataByIdDataLoader byId,
        CancellationToken cancellationToken
    )
    {
        // TODO Use `timestamp` and `locale`.
        return byId.LoadAsync(
            id,
            cancellationToken
        );
    }
}