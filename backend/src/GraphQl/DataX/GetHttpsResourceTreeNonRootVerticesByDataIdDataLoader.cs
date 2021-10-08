using System;
using System.Linq;
using GreenDonut;
using Microsoft.EntityFrameworkCore;

namespace Database.GraphQl.DataX
{
    public sealed class GetHttpsResourceTreeNonRootVerticesByDataIdDataLoader
      : Entities.AssociationsByAssociateIdDataLoader<Data.GetHttpsResource>
    {
        public GetHttpsResourceTreeNonRootVerticesByDataIdDataLoader(
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
                        x.ParentId != null && ids.Contains(x.DataId)
                    ),
                x => x.DataId
                )
        {
        }
    }
}
