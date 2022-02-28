using System;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Collections.Generic;

namespace Database.Configuration
{
    public abstract class AuthConfiguration
    {
        public static void ConfigureServices(
            IServiceCollection services,
            AppSettings appSettings
            )
        {
            // Inspired by https://github.com/openiddict/openiddict-samples/blob/01cb2ce4600cab15867e34826b0287622e6dd71b/samples/Velusia/Velusia.Client/Startup.cs
            services
            .AddAuthentication(_ =>
            {
                _.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(_ =>
            {
                _.AccessDeniedPath = "/unauthorized";
                _.LoginPath = "/users/login";
                _.LogoutPath = "/users/logout";
                _.ReturnUrlParameter = "returnTo";
                _.ExpireTimeSpan = TimeSpan.FromMinutes(50);
                _.SlidingExpiration = false;
            })
            .AddOpenIdConnect(_ =>
            {
                // Note: these settings must match the application details
                // inserted in the database at the server level.
                _.ClientId = "testlab-solar-facades";
                _.ClientSecret = appSettings.OpenIdConnectClientSecret;

                _.RequireHttpsMetadata = false;
                _.GetClaimsFromUserInfoEndpoint = true;
                _.SaveTokens = true;

                // Use the authorization code flow.
                _.ResponseType = OpenIdConnectResponseType.Code;
                _.AuthenticationMethod = OpenIdConnectRedirectBehavior.RedirectGet;

                // Note: setting the Authority allows the OIDC client middleware to automatically
                // retrieve the identity provider's configuration and spare you from setting
                // the different endpoints URIs or the token validation parameters explicitly.
                _.Authority = $"https://{appSettings.MetabaseHost}";

                _.Scope.Add("email");
                _.Scope.Add("roles");

                _.SecurityTokenValidator = new JwtSecurityTokenHandler
                {
                    // Disable the built-in JWT claims mapping feature.
                    InboundClaimTypeMap = new Dictionary<string, string>()
                };

                _.TokenValidationParameters.NameClaimType = "name";
                _.TokenValidationParameters.RoleClaimType = "role";
            });
        }
    }
}