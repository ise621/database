using System;
using System.Threading;
using System.Threading.Tasks;
using Database.Data;
using Database.GraphQl.DataX;
using HotChocolate;

namespace Database.GraphQl.HygrothermalDataX;

public sealed class HygrothermalDataResolvers
{
    public async Task<GetHttpsResource[]> GetGetHttpsResources(
        [Parent] HygrothermalData hygrothermalData,
        GetHttpsResourcesByDataIdDataLoader byId,
        CancellationToken cancellationToken
    )
    {
        return await byId.LoadAsync(hygrothermalData.Id, cancellationToken) ?? [];
    }

    public GetHttpsResourceTree GetGetHttpsResourceTree(
        [Parent] HygrothermalData hygrothermalData
    )
    {
        return new GetHttpsResourceTree(hygrothermalData);
    }

    public DateTime GetTimestamp()
    {
        return DateTime.UtcNow;
    }
}