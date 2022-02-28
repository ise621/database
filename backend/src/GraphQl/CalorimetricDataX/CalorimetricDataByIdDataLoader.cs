using GreenDonut;
using Database.GraphQl.Entities;
using Microsoft.EntityFrameworkCore;

namespace Database.GraphQl.CalorimetricDataX
{
    public sealed class CalorimetricDataByIdDataLoader
      : EntityByIdDataLoader<Data.CalorimetricData>
    {
        public CalorimetricDataByIdDataLoader(
            IBatchScheduler batchScheduler,
            DataLoaderOptions options,
            IDbContextFactory<Data.ApplicationDbContext> dbContextFactory
            )
            : base(
                batchScheduler,
                options,
                dbContextFactory,
                dbContext => dbContext.CalorimetricData
                )
        {
        }
    }
}
