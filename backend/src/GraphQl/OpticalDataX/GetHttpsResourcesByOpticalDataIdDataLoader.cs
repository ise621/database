using System;
using System.Linq;
using GreenDonut;
using Microsoft.EntityFrameworkCore;

namespace Database.GraphQl.OpticalDataX
{
    public sealed class GetHttpsResourcesByOpticalDataIdDataLoader
      : Entities.AssociationsByAssociateIdDataLoader<Data.GetHttpsResource>
    {
        public GetHttpsResourcesByOpticalDataIdDataLoader(
            IBatchScheduler batchScheduler,
            DataLoaderOptions options,
            IDbContextFactory<Data.ApplicationDbContext> dbContextFactory
            )
            : base(
                batchScheduler,
                options,
                dbContextFactory,
                (dbContext, ids) =>
                    dbContext.GetHttpsResources.AsQueryable().Where(x =>
                        ids.Contains(x.DataId)
                    ),
                x => x.DataId
                )
        {
        }
    }
}
