using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Database.Data;
using HotChocolate.Data;
using HotChocolate.Types;
using Guid = System.Guid;

namespace Database.GraphQl.GetHttpsResources;

[ExtendObjectType(nameof(Query))]
public sealed class GetHttpsResourceQueries
{
    [UsePaging]
    // [UseProjection] // We disabled projections because when requesting `id` all results had the same `id` and when also requesting `uuid`, the latter was always the empty UUID `000...`.
    [UseFiltering]
    [UseSorting]
    public IQueryable<GetHttpsResource> GetGetHttpsResources(
        ApplicationDbContext context
    )
    {
        return context.GetHttpsResources;
    }

    public Task<GetHttpsResource?> GetGetHttpsResourceAsync(
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