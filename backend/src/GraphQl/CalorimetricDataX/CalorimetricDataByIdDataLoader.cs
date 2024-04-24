using Database.Data;
using Database.GraphQl.Entities;
using GreenDonut;
using Microsoft.EntityFrameworkCore;

namespace Database.GraphQl.CalorimetricDataX;

public sealed class CalorimetricDataByIdDataLoader
    : EntityByIdDataLoader<CalorimetricData>
{
    public CalorimetricDataByIdDataLoader(
        IBatchScheduler batchScheduler,
        DataLoaderOptions options,
        IDbContextFactory<ApplicationDbContext> dbContextFactory
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