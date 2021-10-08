using System;
using System.Linq;
using GreenDonut;
using Microsoft.EntityFrameworkCore;

namespace Database.GraphQl.GetHttpsResources
{
    public sealed class GetHttpsResourceChildrenByGetHttpsResourceIdDataLoader
      : Entities.AssociationsByAssociateIdDataLoader<Data.GetHttpsResource>
    {
        public GetHttpsResourceChildrenByGetHttpsResourceIdDataLoader(
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
                        ids.Contains(x.ParentId ?? Guid.Empty)
                    ),
                x => x.Id
                )
        {
        }
    }
}
