using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using OpenIddict.Abstractions;

namespace Database.GraphQl.Users
{
    [ExtendObjectType(nameof(Query))]
    public sealed class UserQueries
    {
        public async Task<Data.User?> GetCurrentUserAsync(
            [GlobalState(nameof(ClaimsPrincipal))] ClaimsPrincipal claimsPrincipal,
            Data.ApplicationDbContext context,
            CancellationToken cancellationToken
        )
        {
            if (!claimsPrincipal.HasClaim(ClaimTypes.NameIdentifier))
            {
                return null;
            }
            return await
                context.Users.AsQueryable()
                .SingleOrDefaultAsync(
                    u => u.Subject == claimsPrincipal.GetClaim(ClaimTypes.NameIdentifier),
                    cancellationToken
                ).ConfigureAwait(false);
        }
    }
}