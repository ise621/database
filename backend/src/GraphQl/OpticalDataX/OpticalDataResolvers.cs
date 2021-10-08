using System.Threading;
using System.Threading.Tasks;
using Database.GraphQl.DataX;
using HotChocolate;

namespace Database.GraphQl.OpticalDataX
{
    public sealed class OpticalDataResolvers
    {
        public Task<Data.GetHttpsResource[]> GetGetHttpsResources(
            [Parent] Data.OpticalData opticalData,
            GetHttpsResourcesByDataIdDataLoader byId,
            CancellationToken cancellationToken

        )
        {
            return byId.LoadAsync(opticalData.Id, cancellationToken);
        }

        public GetHttpsResourceTree GetGetHttpsResourceTree(
            [Parent] Data.OpticalData opticalData

        )
        {
            return new GetHttpsResourceTree(opticalData);
        }
    }
}