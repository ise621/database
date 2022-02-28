using System;
using System.Threading;
using System.Threading.Tasks;
using Database.GraphQl.DataX;
using HotChocolate;

namespace Database.GraphQl.PhotovoltaicDataX
{
    public sealed class PhotovoltaicDataResolvers
    {
        public Task<Data.GetHttpsResource[]> GetGetHttpsResources(
            [Parent] Data.PhotovoltaicData photovoltaicData,
            GetHttpsResourcesByDataIdDataLoader byId,
            CancellationToken cancellationToken

        )
        {
            return byId.LoadAsync(photovoltaicData.Id, cancellationToken);
        }

        public GetHttpsResourceTree GetGetHttpsResourceTree(
            [Parent] Data.PhotovoltaicData photovoltaicData
        )
        {
            return new GetHttpsResourceTree(photovoltaicData);
        }

        public DateTime GetTimestamp()
        {
            return DateTime.UtcNow;
        }
    }
}