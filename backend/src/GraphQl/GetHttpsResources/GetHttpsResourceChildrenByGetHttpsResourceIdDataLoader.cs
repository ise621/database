using System;
using System.Linq;
using Database.Data;
using Database.GraphQl.Entities;
using GreenDonut;
using Microsoft.EntityFrameworkCore;

namespace Database.GraphQl.GetHttpsResources;

public sealed class GetHttpsResourceChildrenByGetHttpsResourceIdDataLoader
    : AssociationsByAssociateIdDataLoader<GetHttpsResource>
{
    public GetHttpsResourceChildrenByGetHttpsResourceIdDataLoader(
        IBatchScheduler batchScheduler,
        DataLoaderOptions options,
        IDbContextFactory<ApplicationDbContext> dbContextFactory
    )
        : base(
            batchScheduler,
            options,
            dbContextFactory,
            (dbContext, ids) =>
                dbContext.GetHttpsResources.AsNoTracking().Where(x =>
                    ids.Contains(x.ParentId ?? Guid.Empty)
                ),
            x => x.Id
        )
    {
    }
}