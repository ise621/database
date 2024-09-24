using Database.Data;
using Database.GraphQl.Entities;
using GreenDonut;
using Microsoft.EntityFrameworkCore;

namespace Database.GraphQl.GeometricDataX;

public sealed class GeometricDataByIdDataLoader
    : EntityByIdDataLoader<GeometricData>
{
    public GeometricDataByIdDataLoader(
        IBatchScheduler batchScheduler,
        DataLoaderOptions options,
        IDbContextFactory<ApplicationDbContext> dbContextFactory
    )
        : base(
            batchScheduler,
            options,
            dbContextFactory,
            dbContext => dbContext.GeometricData
        )
    {
    }
}