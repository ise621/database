using System;
using System.Linq;
using Database.Data;
using Database.GraphQl.Entities;
using GreenDonut;
using Microsoft.EntityFrameworkCore;

namespace Database.GraphQl.DataX;

public sealed class GetHttpsResourceTreeRootByDataIdDataLoader
    : AssociationsByAssociateIdDataLoader<GetHttpsResource>
{
    public GetHttpsResourceTreeRootByDataIdDataLoader(
        IBatchScheduler batchScheduler,
        DataLoaderOptions options,
        IDbContextFactory<ApplicationDbContext> dbContextFactory
    )
        : base(
            batchScheduler,
            options,
            dbContextFactory,
            (dbContext, ids) =>
                dbContext.GetHttpsResources.AsQueryable().Where(x =>
                    x.ParentId == null && ids.Contains(x.CalorimetricDataId ??
                                                       x.HygrothermalDataId ?? x.OpticalDataId ??
                                                       x.PhotovoltaicDataId ?? Guid.Empty)
                ),
            x => x.DataId
        )
    {
    }
}