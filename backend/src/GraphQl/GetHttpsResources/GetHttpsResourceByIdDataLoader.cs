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
            DataLoaderOptions options,
            IDbContextFactory<Data.ApplicationDbContext> dbContextFactory
            )
            : base(
                batchScheduler,
                options,
                dbContextFactory,
                dbContext => dbContext.GetHttpsResources
                )
        {
        }
    }
}
