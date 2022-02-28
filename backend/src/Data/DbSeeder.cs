using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Database.Data
{
    public sealed class DbSeeder
    {
        public static async Task DoAsync(
            IServiceProvider services
            )
        {
            var logger = services.GetRequiredService<ILogger<DbSeeder>>();
            logger.LogDebug("Seeding the database");
            // var environment = services.GetRequiredService<IWebHostEnvironment>();
            // var appSettings = services.GetRequiredService<AppSettings>();
        }
    }
}