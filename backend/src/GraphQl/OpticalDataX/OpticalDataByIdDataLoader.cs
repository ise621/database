using GreenDonut;
using Database.GraphQl.Entities;
using Microsoft.EntityFrameworkCore;

namespace Database.GraphQl.OpticalDataX
{
    public sealed class OpticalDataByIdDataLoader
      : EntityByIdDataLoader<Data.OpticalData>
    {
        public OpticalDataByIdDataLoader(
            IBatchScheduler batchScheduler,
            DataLoaderOptions options,
            IDbContextFactory<Data.ApplicationDbContext> dbContextFactory
            )
            : base(
                batchScheduler,
                options,
                dbContextFactory,
                dbContext => dbContext.OpticalData
                )
        {
        }
    }
}
