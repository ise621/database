using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Database.Data;
using Database.GraphQl.CalorimetricDataX;
using Database.GraphQl.HygrothermalDataX;
using Database.GraphQl.OpticalDataX;
using Database.GraphQl.PhotovoltaicDataX;
using Database.GraphQl.GeometricDataX;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;

namespace Database.GraphQl.DataX;

[ExtendObjectType(nameof(Query))]
public sealed class DataQueries
{
    [UsePaging]
    // [UseProjection] // We disabled projections because when requesting `id` all results had the same `id` and when also requesting `uuid`, the latter was always the empty UUID `000...`.
    [UseFiltering] // TODO Filtering does not work with unions.
    [UseSorting]
    public IEnumerable<IData> GetAllData(
        DateTime? timestamp,
        [GraphQLType<LocaleType>] string? locale,
        ApplicationDbContext context
    )
    {
        // TODO Use `timestamp` and `locale`.
        // return context.CalorimetricData.AsNoTracking<Data.IData>();
        // The union below does sadly not work because the different kinds of data have different include operations.
        // return context.CalorimetricData.AsNoTracking<Data.IData>()
        //     .Union(context.HygrothermalData.AsNoTracking<Data.IData>())
        //     .Union(context.OpticalData.AsNoTracking<Data.IData>())
        //     .Union(context.PhotovoltaicData.AsNoTracking<Data.IData>());
        return context.CalorimetricData.AsNoTracking<IData>().AsEnumerable()
            .Concat(context.HygrothermalData.AsNoTracking<IData>().AsEnumerable())
            .Concat(context.OpticalData.AsNoTracking<IData>().AsEnumerable())
            .Concat(context.PhotovoltaicData.AsNoTracking<IData>().AsEnumerable())
            .Concat(context.GeometricData.AsNoTracking<IData>().AsEnumerable());
    }

    public async Task<IData?> GetDataAsync(
        Guid id,
        DateTime? timestamp,
        [GraphQLType<LocaleType>] string? locale,
        CalorimetricDataByIdDataLoader calorimetricDataById,
        HygrothermalDataByIdDataLoader hygrothermalDataById,
        OpticalDataByIdDataLoader opticalDataById,
        PhotovoltaicDataByIdDataLoader photovoltaicDataById,
        GeometricDataByIdDataLoader geometricDataById,
        CancellationToken cancellationToken
    )
    {
        // TODO Use `timestamp` and `locale`.
        return
            await calorimetricDataById.LoadAsync(
                id,
                cancellationToken
            ).ConfigureAwait(false) ??
            await hygrothermalDataById.LoadAsync(
                id,
                cancellationToken
            ).ConfigureAwait(false) ??
            await opticalDataById.LoadAsync(
                id,
                cancellationToken
            ).ConfigureAwait(false) ??
            await geometricDataById.LoadAsync(
                id,
                cancellationToken
            ).ConfigureAwait(false) ??
            await photovoltaicDataById.LoadAsync(
                id,
                cancellationToken
            ).ConfigureAwait(false) as IData;
    }
}