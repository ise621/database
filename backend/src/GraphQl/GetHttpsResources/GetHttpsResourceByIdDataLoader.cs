using GreenDonut;
using Database.GraphQl.Entities;
using Microsoft.EntityFrameworkCore;

namespace Database.GraphQl.GetHttpsResources
{
    public sealed class GetHttpsResourceByIdDataLoader
      : EntityByIdDataLoader<Data.GetHttpsResource>
    {
        public GetHttpsResourceByIdDataLoader(
            IBatchScheduler batchScheduler,
            IDbContextFactory<Data.ApplicationDbContext> dbContextFactory
            )
            : base(
                batchScheduler,
                dbContextFactory,
                dbContext => dbContext.GetHttpsResources
                )
        {
        }
    }
}
