using System;
using System.Threading;
using System.Threading.Tasks;
using Database.Data;
using Database.GraphQl.DataX;
using HotChocolate;

namespace Database.GraphQl.CalorimetricDataX;

public sealed class CalorimetricDataResolvers
{
    public Task<GetHttpsResource[]> GetGetHttpsResources(
        [Parent] CalorimetricData calorimetricData,
        GetHttpsResourcesByDataIdDataLoader byId,
        CancellationToken cancellationToken
    )
    {
        return byId.LoadAsync(calorimetricData.Id, cancellationToken);
    }

    public GetHttpsResourceTree GetGetHttpsResourceTree(
        [Parent] CalorimetricData calorimetricData
    )
    {
        return new GetHttpsResourceTree(calorimetricData);
    }

    public DateTime GetTimestamp()
    {
        return DateTime.UtcNow;
    }
}