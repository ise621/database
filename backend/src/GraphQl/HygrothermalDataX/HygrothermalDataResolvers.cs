using System;
using System.Threading;
using System.Threading.Tasks;
using Database.GraphQl.DataX;
using HotChocolate;

namespace Database.GraphQl.HygrothermalDataX
{
    public sealed class HygrothermalDataResolvers
    {
        public Task<Data.GetHttpsResource[]> GetGetHttpsResources(
            [Parent] Data.HygrothermalData hygrothermalData,
            GetHttpsResourcesByDataIdDataLoader byId,
            CancellationToken cancellationToken

        )
        {
            return byId.LoadAsync(hygrothermalData.Id, cancellationToken);
        }

        public GetHttpsResourceTree GetGetHttpsResourceTree(
            [Parent] Data.HygrothermalData hygrothermalData
        )
        {
            return new GetHttpsResourceTree(hygrothermalData);
        }

        public DateTime GetTimestamp()
        {
            return DateTime.UtcNow;
        }
    }
}