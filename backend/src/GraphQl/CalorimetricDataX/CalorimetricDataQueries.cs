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

namespace Database.GraphQl.CalorimetricDataX;

[ExtendObjectType(nameof(Query))]
public sealed class CalorimetricDataQueries
{
    [UsePaging]
    // [UseProjection] // We disabled projections because when requesting `id` all results had the same `id` and when also requesting `uuid`, the latter was always the empty UUID `000...`.
    [UseFiltering]
    [UseSorting]
    public IQueryable<CalorimetricData> GetAllCalorimetricData(
        [GraphQLType<LocaleType>] string? locale,
        ApplicationDbContext context,
        ISortingContext sorting
    )
    {
        sorting.StabilizeOrder<CalorimetricData>();
        // TODO Use `locale`.
        return context.CalorimetricData.AsNoTracking();
    }

    public Task<CalorimetricData?> GetCalorimetricDataAsync(
        Guid id,
        [GraphQLType<LocaleType>] string? locale,
        CalorimetricDataByIdDataLoader byId,
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