using Database.Data;
using Database.GraphQl.Entities;
using GreenDonut;
using Microsoft.EntityFrameworkCore;

namespace Database.GraphQl.GetHttpsResources;

public sealed class GetHttpsResourceByIdDataLoader
    : EntityByIdDataLoader<GetHttpsResource>
{
    public GetHttpsResourceByIdDataLoader(
        IBatchScheduler batchScheduler,
        DataLoaderOptions options,
        IDbContextFactory<ApplicationDbContext> dbContextFactory
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