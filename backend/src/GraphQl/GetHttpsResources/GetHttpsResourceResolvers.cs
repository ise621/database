using System;
using System.Threading;
using System.Threading.Tasks;
using Database.GraphQl.CalorimetricDataX;
using Database.GraphQl.HygrothermalDataX;
using Database.GraphQl.OpticalDataX;
using Database.GraphQl.PhotovoltaicDataX;
using HotChocolate;

namespace Database.GraphQl.GetHttpsResources
{
    public sealed class GetHttpsResourceResolvers
    {
        public async Task<Data.IData?> GetData(
            [Parent] Data.GetHttpsResource getHttpsResource,
            CalorimetricDataByIdDataLoader calorimetricDataById,
            HygrothermalDataByIdDataLoader hygrothermalDataById,
            OpticalDataByIdDataLoader opticalDataById,
            PhotovoltaicDataByIdDataLoader photovoltaicDataById,
            CancellationToken cancellationToken

        )
        {
            if (getHttpsResource.CalorimetricDataId is not null)
            {
                return await calorimetricDataById.LoadAsync(
                    getHttpsResource.CalorimetricDataId ?? throw new Exception("Impossible!"),
                    cancellationToken
                ).ConfigureAwait(false);
            }
            if (getHttpsResource.HygrothermalDataId is not null)
            {
                return await hygrothermalDataById.LoadAsync(
                    getHttpsResource.HygrothermalDataId ?? throw new Exception("Impossible!"),
                    cancellationToken
                ).ConfigureAwait(false);
            }
            if (getHttpsResource.OpticalDataId is not null)
            {
                return await opticalDataById.LoadAsync(
                    getHttpsResource.OpticalDataId ?? throw new Exception("Impossible!"),
                    cancellationToken
                ).ConfigureAwait(false);
            }
            if (getHttpsResource.PhotovoltaicDataId is not null)
            {
                return await photovoltaicDataById.LoadAsync(
                    getHttpsResource.PhotovoltaicDataId ?? throw new Exception("Impossible!"),
                    cancellationToken
                ).ConfigureAwait(false);
            }
            return null;
        }

        public Uri GetLocator(
            [Parent] Data.GetHttpsResource getHttpsResource,
            [Service] AppSettings appSettings
        )
        {
            return new Uri($"{appSettings.Host}/api/resources/{getHttpsResource.Id}");
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