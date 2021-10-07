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
            IDbContextFactory<Data.ApplicationDbContext> dbContextFactory
            )
            : base(
                batchScheduler,
                dbContextFactory,
                dbContext => dbContext.OpticalData
                )
        {
        }
    }
}
