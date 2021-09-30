using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using OpenIddict.Abstractions;
using OpenIddict.Validation.AspNetCore;
using Quartz;
using System.Linq;
using System.Security.Cryptography;
using System.Reflection;
using System.Security;
using System.IO;

namespace Database.Configuration
{
    public abstract class AuthConfiguration
    {
        // public const string JwtBearerAuthenticatedPolicy = "JwtBearerAuthenticated";
        public const string ReadPolicy = "Read";
        public const string WritePolicy = "Write";
        public static string ReadApiScope { get; } = "api:read";
        public static string WriteApiScope { get; } = "api:write";

        public static void ConfigureServices(
            IServiceCollection services,
            IWebHostEnvironment environment,
            AppSettings appSettings
            )
        {
            // TODO How can we use `SecureString` for passwords set via environment variables?
            /* ConfigureIdentityServices(services); */
            /* ConfigureAuthenticiationAndAuthorizationServices(services, environment, appSettings, encryptionCertificate, signingCertificate); */
            /* ConfigureTaskScheduling(services, environment); */
            /* ConfigureOpenIddictServices(services, environment, appSettings, encryptionCertificate, signingCertificate); */
        }

        private static X509Certificate2 LoadCertificate(
            string fileName,
            string password
        )
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new Exception($"Empty password for certificate {fileName}.");
            }
            var stream =
                Assembly.GetExecutingAssembly().GetManifestResourceStream($"Database.{fileName}")
                ?? throw new Exception($"Missing certificate {fileName}.");
            using var buffer = new MemoryStream();
            stream.CopyTo(buffer);
            return new X509Certificate2(
                buffer.ToArray(),
                password,
                X509KeyStorageFlags.EphemeralKeySet
            );
        }

        private static void ConfigureIdentityServices(
            IServiceCollection services
            )
        {
        }

        private static void ConfigureAuthenticiationAndAuthorizationServices(
            IServiceCollection services,
            IWebHostEnvironment environment,
            AppSettings appSettings,
            X509Certificate2 encryptionCertificate,
            X509Certificate2 signingCertificate
            )
        {
        }

        private static void ConfigureTaskScheduling(
          IServiceCollection services,
            IWebHostEnvironment environment
        )
        {
            // OpenIddict offers native integration with Quartz.NET to perform scheduled tasks
            // (like pruning orphaned authorizations/tokens from the database) at regular intervals.
            // For configuring Quartz see
            // https://www.quartz-scheduler.net/documentation/quartz-3.x/packages/hosted-services-integration.html
            services.AddQuartz(_ =>
            {
                _.SchedulerId = "database";
                _.SchedulerName = "Database";
                _.UseMicrosoftDependencyInjectionJobFactory();
                _.UseSimpleTypeLoader();
                _.UseInMemoryStore();
                _.UseDefaultThreadPool(_ =>
                    _.MaxConcurrency = 10
                );
                if (environment.IsEnvironment("test"))
                {
                    // See https://gitter.im/MassTransit/MassTransit?at=5db2d058f6db7f4f856fb404
                    _.SchedulerName = Guid.NewGuid().ToString();
                }
            });
            // Register the Quartz.NET service and configure it to block shutdown until jobs are complete.
            services.AddQuartzHostedService(_ =>
                _.WaitForJobsToComplete = true
                );
        }

        private static void ConfigureOpenIddictServices(
            IServiceCollection services,
            IWebHostEnvironment environment,
            AppSettings appSettings,
            X509Certificate2 encryptionCertificate,
            X509Certificate2 signingCertificate
            )
        {
        }
    }
}