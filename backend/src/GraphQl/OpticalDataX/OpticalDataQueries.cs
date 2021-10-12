using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using Guid = System.Guid;

namespace Database.GraphQl.OpticalDataX
{
    [ExtendObjectType(nameof(Query))]
    public sealed class OpticalDataQueries
    {
        [UseDbContext(typeof(Data.ApplicationDbContext))]
        [UsePaging]
        // [UseProjection] // We disabled projections because when requesting `id` all results had the same `id` and when also requesting `uuid`, the latter was always the empty UUID `000...`.
        [UseFiltering]
        [UseSorting]
        public IQueryable<Data.OpticalData> GetAllOpticalData(
            DateTime? timestamp,
            [GraphQLType(typeof(LocaleType))] string? locale,
            [ScopedService] Data.ApplicationDbContext context
            )
        {
            // TODO Use `timestamp` and `locale`.
            return context.OpticalData;
        }

        public Task<Data.OpticalData?> GetOpticalDataAsync(
            Guid id,
            DateTime? timestamp,
            [GraphQLType(typeof(LocaleType))] string? locale,
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
}
