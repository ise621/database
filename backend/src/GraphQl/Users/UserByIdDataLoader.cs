using GreenDonut;
using Database.GraphQl.Entities;
using Microsoft.EntityFrameworkCore;

namespace Database.GraphQl.Users
{
    public sealed class UserByIdDataLoader
      : EntityByIdDataLoader<Data.User>
    {
        public UserByIdDataLoader(
            IBatchScheduler batchScheduler,
            DataLoaderOptions options,
            IDbContextFactory<Data.ApplicationDbContext> dbContextFactory
            )
            : base(
                batchScheduler,
                options,
                dbContextFactory,
                dbContext => dbContext.Users
                )
        {
        }
    }
}
