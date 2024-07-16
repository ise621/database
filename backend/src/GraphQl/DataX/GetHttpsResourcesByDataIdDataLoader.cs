using System;
using System.Linq;
using Database.Data;
using Database.GraphQl.Entities;
using GreenDonut;
using Microsoft.EntityFrameworkCore;

namespace Database.GraphQl.DataX;

public sealed class GetHttpsResourcesByDataIdDataLoader
    : AssociationsByAssociateIdDataLoader<GetHttpsResource>
{
    public GetHttpsResourcesByDataIdDataLoader(
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
                    ids.Contains(x.CalorimetricDataId ??
                                 x.HygrothermalDataId ?? x.OpticalDataId ?? x.PhotovoltaicDataId ?? x.GeometricDataId ?? Guid.Empty)
                ),
            x => x.DataId
        )
    {
    }
}