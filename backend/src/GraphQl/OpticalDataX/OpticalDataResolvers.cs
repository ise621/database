using System.Threading;
using System.Threading.Tasks;
using HotChocolate;

namespace Database.GraphQl.OpticalDataX
{
    public sealed class OpticalDataResolvers
    {
        public Task<Data.GetHttpsResource[]> GetGetHttpsResources(
            [Parent] Data.OpticalData opticalData,
            GetHttpsResourcesByOpticalDataIdDataLoader byId,
            CancellationToken cancellationToken

        )
        {
            return byId.LoadAsync(opticalData.Id, cancellationToken);
        }
    }
}