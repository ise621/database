using System;
using System.Threading;
using System.Threading.Tasks;
using Database.Data;
using Database.GraphQl.DataX;
using HotChocolate;

namespace Database.GraphQl.GeometricDataX;

public sealed class GeometricDataResolvers
{
    public Task<GetHttpsResource[]> GetGetHttpsResources(
        [Parent] GeometricData geometricData,
        GetHttpsResourcesByDataIdDataLoader byId,
        CancellationToken cancellationToken
    )
    {
        return byId.LoadAsync(geometricData.Id, cancellationToken);
    }

    public GetHttpsResourceTree GetGetHttpsResourceTree(
        [Parent] GeometricData geometricData
    )
    {
        return new GetHttpsResourceTree(geometricData);
    }

    public DateTime GetTimestamp()
    {
        return DateTime.UtcNow;
    }
}