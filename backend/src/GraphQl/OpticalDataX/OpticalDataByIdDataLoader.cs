using Database.Data;
using Database.GraphQl.Entities;
using GreenDonut;
using Microsoft.EntityFrameworkCore;

namespace Database.GraphQl.OpticalDataX;

public sealed class OpticalDataByIdDataLoader
    : EntityByIdDataLoader<OpticalData>
{
    public OpticalDataByIdDataLoader(
        IBatchScheduler batchScheduler,
        DataLoaderOptions options,
        IDbContextFactory<ApplicationDbContext> dbContextFactory
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