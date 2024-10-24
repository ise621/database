using System;
using System.Threading;
using System.Threading.Tasks;
using Database.Data;
using Database.GraphQl.DataX;
using HotChocolate;

namespace Database.GraphQl.OpticalDataX;

public sealed class OpticalDataResolvers
{
    public async Task<GetHttpsResource[]> GetGetHttpsResources(
        [Parent] OpticalData opticalData,
        GetHttpsResourcesByDataIdDataLoader byId,
        CancellationToken cancellationToken
    )
    {
        return await byId.LoadAsync(opticalData.Id, cancellationToken) ?? [];
    }

    public GetHttpsResourceTree GetGetHttpsResourceTree(
        [Parent] OpticalData opticalData
    )
    {
        return new GetHttpsResourceTree(opticalData);
    }

    public DateTime GetTimestamp()
    {
        return DateTime.UtcNow;
    }
}