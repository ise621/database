using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Database.Configuration;
using Database.Data.Extensions;
using HotChocolate.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Npgsql;
using Serilog;

namespace Database
{
    public sealed class Startup
    {
        private readonly IWebHostEnvironment _environment;
        private readonly AppSettings _appSettings;

        public Startup(
            IWebHostEnvironment environment,
            IConfiguration configuration
            )
        {
            _environment = environment;
            _appSettings = configuration.Get<AppSettings>();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            AuthConfiguration.ConfigureServices(services, _appSettings);
            GraphQlConfiguration.ConfigureServices(services, _environment);
            ConfigureDatabaseServices(services);
            ConfigureMessageSenderServices(services);
            ConfigureRequestResponseServices(services);
            ConfigureSessionServices(services);
            services.AddHttpClient();
            services
                .AddHealthChecks()
                .AddDbContextCheck<Data.ApplicationDbContext>();
            services.AddSingleton(_appSettings);
            services.AddSingleton(_environment);
            // services.AddDatabaseDeveloperPageExceptionFilter();
        }

        private static void ConfigureRequestResponseServices(IServiceCollection services)
        {
            // https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/proxy-load-balancer#forwarded-headers-middleware-order
            services.Configure<ForwardedHeadersOptions>(_ =>
            {
                // TODO _.AllowedHosts = ...
                _.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
                // https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/proxy-load-balancer#forward-the-scheme-for-linux-and-non-iis-reverse-proxies
                _.KnownNetworks.Clear();
                _.KnownProxies.Clear();
            }
            );
            services.AddCors(_ =>
                _.AddDefaultPolicy(policy =>
                    policy
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    )
                );
            services.AddControllersWithViews();
        }

        private void ConfigureMessageSenderServices(IServiceCollection services)
        {
            services.AddTransient<Services.IEmailSender>(serviceProvider =>
                new Services.EmailSender(
                    _appSettings.Email.SmtpHost,
                    _appSettings.Email.SmtpPort,
                    serviceProvider.GetRequiredService<Microsoft.Extensions.Logging.ILogger<Services.EmailSender>>()
                )
            );
        }

        private static void ConfigureSessionServices(IServiceCollection services)
        {
            // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/app-state#session-state
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
                {
                    // Set a short timeout for easy testing.
                    options.IdleTimeout = TimeSpan.FromSeconds(10);
                    options.Cookie.HttpOnly = true;
                    // Make the session cookie essential
                    options.Cookie.IsEssential = true;
                });
        }

        private void ConfigureDatabaseServices(IServiceCollection services)
        {
            services.AddPooledDbContextFactory<Data.ApplicationDbContext>(options =>
                {
                    var dataSourceBuilder = new NpgsqlDataSourceBuilder(_appSettings.Database.ConnectionString);
                    dataSourceBuilder.MapEnum<Enumerations.DataKind>();
                    options
                    .UseNpgsql(dataSourceBuilder.Build() /*, optionsBuilder => optionsBuilder.UseNodaTime() */)
                    .UseSchemaName(_appSettings.Database.SchemaName);
                    if (!_environment.IsProduction())
                    {
                        options
                        .EnableSensitiveDataLogging()
                        .EnableDetailedErrors();
                    }
                }
                );
            // Database context as services are used by `Identity`.
            services.AddDbContext<Data.ApplicationDbContext>(
                (services, options) =>
                {
                    if (!_environment.IsProduction())
                    {
                        options
                        .EnableSensitiveDataLogging()
                        .EnableDetailedErrors();
                    }
                    services
                    .GetRequiredService<IDbContextFactory<Data.ApplicationDbContext>>()
                    .CreateDbContext();
                },
                ServiceLifetime.Transient
                );
        }

        public void Configure(IApplicationBuilder app)
        {
            // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/middleware/
            if (_environment.IsDevelopment() || _environment.IsEnvironment("test"))
            {
                app.UseDeveloperExceptionPage();
                // app.UseMigrationsEndPoint();
                // Forwarded Headers Middleware must run before other middleware except diagnostics and error handling middleware. In particular before HSTS middleware.
                // See https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/proxy-load-balancer#other-proxy-server-and-load-balancer-scenarios
                app.UseForwardedHeaders();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // Forwarded Headers Middleware must run before other middleware except diagnostics and error handling middleware. In particular before HSTS middleware.
                // See https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/proxy-load-balancer#other-proxy-server-and-load-balancer-scenarios
                app.UseForwardedHeaders();
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                // ASP.NET advices to not use HSTS for APIs, see the warning on
                // https://docs.microsoft.com/en-us/aspnet/core/security/enforcing-ssl
                // app.UseHsts(); // Done by NGINX, see https://www.nginx.com/blog/http-strict-transport-security-hsts-and-nginx/
            }
            // app.UseStatusCodePages();
            // app.UseHttpsRedirection(); // Done by NGINX
            app.UseSerilogRequestLogging();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseRouting();
            // TODO Do we really want this? See https://docs.microsoft.com/en-us/aspnet/core/fundamentals/localization?view=aspnetcore-5.0
            app.UseRequestLocalization(_ =>
            {
                _.AddSupportedCultures("en-US", "de-DE");
                _.AddSupportedUICultures("en-US", "de-DE");
                _.SetDefaultCulture("en-US");
            });
            app.UseCors();
            // app.UseCertificateForwarding(); // https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/proxy-load-balancer?view=aspnetcore-5.0#other-web-proxies
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            // app.UseResponseCompression(); // Done by Nginx
            // app.UseResponseCaching(); // Done by Nginx
            /* app.UseWebSockets(); */
            app.UseEndpoints(_ =>
            {
                _.MapGraphQL().WithOptions(
                    // https://chillicream.com/docs/hotchocolate/server/middleware
                    new GraphQLServerOptions
                    {
                        EnableSchemaRequests = true,
                        EnableGetRequests = false,
                        // AllowedGetOperations = AllowedGetOperations.Query
                        EnableMultipartRequests = false,
                        Tool = {
                            DisableTelemetry = true,
                            Enable = true, // _environment.IsDevelopment()
                            IncludeCookies = false,
                            GraphQLEndpoint = "/graphql",
                            HttpMethod = DefaultHttpMethod.Post,
                            Title = "GraphQL"
                        }
                    }
                );
                _.MapControllers();
                _.MapHealthChecks("/health",
                    new HealthCheckOptions
                    {
                        ResponseWriter = WriteJsonResponse
                    }
                );
            });
        }

        // Inspired by https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/health-checks?view=aspnetcore-7.0#customize-output
        private static Task WriteJsonResponse(HttpContext context, HealthReport healthReport)
{
    context.Response.ContentType = "application/json; charset=utf-8";

    var options = new JsonWriterOptions { Indented = true };

    using var memoryStream = new MemoryStream();
    using (var jsonWriter = new Utf8JsonWriter(memoryStream, options))
    {
        jsonWriter.WriteStartObject();
        jsonWriter.WriteString("status", healthReport.Status.ToString());
        jsonWriter.WriteStartObject("results");

        foreach (var healthReportEntry in healthReport.Entries)
        {
            jsonWriter.WriteStartObject(healthReportEntry.Key);
            jsonWriter.WriteString("status",
                healthReportEntry.Value.Status.ToString());
            jsonWriter.WriteString("description",
                healthReportEntry.Value.Description);
            jsonWriter.WriteStartObject("data");

            foreach (var item in healthReportEntry.Value.Data)
            {
                jsonWriter.WritePropertyName(item.Key);

                JsonSerializer.Serialize(jsonWriter, item.Value,
                    item.Value?.GetType() ?? typeof(object));
            }

            jsonWriter.WriteEndObject();
            jsonWriter.WriteEndObject();
        }

        jsonWriter.WriteEndObject();
        jsonWriter.WriteEndObject();
    }

    return context.Response.WriteAsync(
        Encoding.UTF8.GetString(memoryStream.ToArray()));
}
    }
}