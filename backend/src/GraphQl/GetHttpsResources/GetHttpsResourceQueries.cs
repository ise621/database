using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using Guid = System.Guid;

namespace Database.GraphQl.GetHttpsResources
{
    [ExtendObjectType(nameof(Query))]
    public sealed class GetHttpsResourceQueries
    {
        [UseDbContext(typeof(Data.ApplicationDbContext))]
        [UsePaging]
        // [UseProjection] // We disabled projections because when requesting `id` all results had the same `id` and when also requesting `uuid`, the latter was always the empty UUID `000...`.
        [UseFiltering]
        [UseSorting]
        public IQueryable<Data.GetHttpsResource> GetGetHttpsResources(
            Data.ApplicationDbContext context
            )
        {
            return context.GetHttpsResources;
        }

        public Task<Data.GetHttpsResource?> GetGetHttpsResourceAsync(
            Guid id,
            GetHttpsResourceByIdDataLoader byId,
            CancellationToken cancellationToken
            )
        {
            return byId.LoadAsync(
                id,
                cancellationToken
                );
        }
    }
}
