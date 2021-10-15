using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Database.GraphQl.CalorimetricDataX;
using Database.GraphQl.HygrothermalDataX;
using Database.GraphQl.OpticalDataX;
using Database.GraphQl.PhotovoltaicDataX;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using Guid = System.Guid;

namespace Database.GraphQl.DataX
{
    [ExtendObjectType(nameof(Query))]
    public sealed class DataQueries
    {
        [UseDbContext(typeof(Data.ApplicationDbContext))]
        [UsePaging]
        // [UseProjection] // We disabled projections because when requesting `id` all results had the same `id` and when also requesting `uuid`, the latter was always the empty UUID `000...`.
        [UseFiltering]
        [UseSorting]
        public IQueryable<Data.IData> GetAllData(
            DateTime? timestamp,
            [GraphQLType(typeof(LocaleType))] string? locale,
            [ScopedService] Data.ApplicationDbContext context
            )
        {
            // TODO Use `timestamp` and `locale`.
            return context.CalorimetricData.AsQueryable<Data.IData>();
            // TODO Why does the union below not work?
            // return context.CalorimetricData.AsQueryable<Data.IData>()
            //     .Union(context.HygrothermalData.AsQueryable<Data.IData>())
            //     .Union(context.OpticalData.AsQueryable<Data.IData>())
            //     .Union(context.PhotovoltaicData.AsQueryable<Data.IData>());
        }

        public async Task<Data.IData?> GetDataAsync(
            Guid id,
            DateTime? timestamp,
            [GraphQLType(typeof(LocaleType))] string? locale,
            CalorimetricDataByIdDataLoader calorimetricDataById,
            HygrothermalDataByIdDataLoader hygrothermalDataById,
            OpticalDataByIdDataLoader opticalDataById,
            PhotovoltaicDataByIdDataLoader photovoltaicDataById,
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
                (await photovoltaicDataById.LoadAsync(
                    id,
                    cancellationToken
                ).ConfigureAwait(false) as Data.IData);
        }
    }
}
