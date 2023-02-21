using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using Guid = System.Guid;

namespace Database.GraphQl.PhotovoltaicDataX
{
    [ExtendObjectType(nameof(Query))]
    public sealed class PhotovoltaicDataQueries
    {
        [UseDbContext(typeof(Data.ApplicationDbContext))]
        [UsePaging]
        // [UseProjection] // We disabled projections because when requesting `id` all results had the same `id` and when also requesting `uuid`, the latter was always the empty UUID `000...`.
        [UseFiltering]
        [UseSorting]
        public IQueryable<Data.PhotovoltaicData> GetAllPhotovoltaicData(
            DateTime? timestamp,
            [GraphQLType<LocaleType>] string? locale,
            Data.ApplicationDbContext context
            )
        {
            // TODO Use `timestamp` and `locale`.
            return context.PhotovoltaicData;
        }

        public Task<Data.PhotovoltaicData?> GetPhotovoltaicDataAsync(
            Guid id,
            DateTime? timestamp,
            [GraphQLType<LocaleType>] string? locale,
            PhotovoltaicDataByIdDataLoader byId,
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
