using System;
using System.Threading;
using System.Threading.Tasks;
using Database.GraphQl.OpticalDataX;
using HotChocolate;

namespace Database.GraphQl.GetHttpsResources
{
    public sealed class GetHttpsResourceResolvers
    {
        // TODO Support non-optical data.
        public Task<Data.OpticalData> GetData(
            [Parent] Data.GetHttpsResource getHttpsResource,
            OpticalDataByIdDataLoader byId,
            CancellationToken cancellationToken

        )
        {
            return byId.LoadAsync(getHttpsResource.Id, cancellationToken)!;
        }

        public Uri GetLocator(
            [Parent] Data.GetHttpsResource getHttpsResource,
            [Service] AppSettings appSettings
        )
        {
            // TODO Why is `?? Guid.Empty` below necessary although `getHttpsResource.ParentId` is not null?
            return new Uri($"{appSettings.Host}/files/{getHttpsResource.Id}");
        }

        public async Task<Data.GetHttpsResource?> GetParent(
            [Parent] Data.GetHttpsResource getHttpsResource,
            GetHttpsResourceByIdDataLoader byId,
            CancellationToken cancellationToken

        )
        {
            // TODO Why is `?? Guid.Empty` below necessary although `getHttpsResource.ParentId` is not null?
            return getHttpsResource.ParentId is null
                ? null
                : await byId.LoadAsync(getHttpsResource.ParentId ?? Guid.Empty, cancellationToken)!;
        }

        public Task<Data.GetHttpsResource[]> GetChildren(
            [Parent] Data.GetHttpsResource getHttpsResource,
            GetHttpsResourceChildrenByGetHttpsResourceIdDataLoader byId,
            CancellationToken cancellationToken

        )
        {
            return byId.LoadAsync(getHttpsResource.Id, cancellationToken)!;
        }
    }
}