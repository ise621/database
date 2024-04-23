using Database.Data;
using Database.GraphQl.Entities;
using GreenDonut;
using Microsoft.EntityFrameworkCore;

namespace Database.GraphQl.PhotovoltaicDataX;

public sealed class PhotovoltaicDataByIdDataLoader
    : EntityByIdDataLoader<PhotovoltaicData>
{
    public PhotovoltaicDataByIdDataLoader(
        IBatchScheduler batchScheduler,
        DataLoaderOptions options,
        IDbContextFactory<ApplicationDbContext> dbContextFactory
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