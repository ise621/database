using GreenDonut;
using Database.GraphQl.Entities;
using Microsoft.EntityFrameworkCore;

namespace Database.GraphQl.HygrothermalDataX
{
    public sealed class HygrothermalDataByIdDataLoader
      : EntityByIdDataLoader<Data.HygrothermalData>
    {
        public HygrothermalDataByIdDataLoader(
            IBatchScheduler batchScheduler,
            DataLoaderOptions options,
            IDbContextFactory<Data.ApplicationDbContext> dbContextFactory
            )
            : base(
                batchScheduler,
                options,
                dbContextFactory,
                dbContext => dbContext.HygrothermalData
                )
        {
        }
    }
}
