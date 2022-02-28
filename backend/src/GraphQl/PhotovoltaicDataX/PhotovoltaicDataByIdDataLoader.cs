using GreenDonut;
using Database.GraphQl.Entities;
using Microsoft.EntityFrameworkCore;

namespace Database.GraphQl.PhotovoltaicDataX
{
    public sealed class PhotovoltaicDataByIdDataLoader
      : EntityByIdDataLoader<Data.PhotovoltaicData>
    {
        public PhotovoltaicDataByIdDataLoader(
            IBatchScheduler batchScheduler,
            DataLoaderOptions options,
            IDbContextFactory<Data.ApplicationDbContext> dbContextFactory
            )
            : base(
                batchScheduler,
                options,
                dbContextFactory,
                dbContext => dbContext.PhotovoltaicData
                )
        {
        }
    }
}
