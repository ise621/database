using System;
using System.Threading.Tasks;
using Database.Configuration;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Database.Controllers;

// For gotchas regarding antiforgery tokens read
// [Clarity around IAntiforgery and ValidateAntiForgeryToken](https://github.com/dotnet/aspnetcore/issues/2783)
public sealed class AntiforgeryController(IAntiforgery antiforgeryService) : Controller
{
    private const string XsrfCookieKey = "XSRF-TOKEN";

    private readonly IAntiforgery _antiforgeryService = antiforgeryService;

    private readonly CookieOptions _xsrfCookieOptions =
        new()
        {
            HttpOnly = false,
            SameSite = SameSiteMode.Strict
        };

    [EndpointName("AntiforgeryToken")]
    [EndpointDescription("Get an antiforgery token.")]
    [HttpGet("~/antiforgery/token")]
    public async Task<IActionResult> Token()
    {
        await HttpContextAuthentication.Authenticate(HttpContext).ConfigureAwait(false);
        var tokens = _antiforgeryService.GetAndStoreTokens(HttpContext);
        HttpContext.Response.Cookies.Append(
            XsrfCookieKey,
            tokens.RequestToken ??
            throw new InvalidOperationException(
                "The request token supposed to be generated by the antiforgery service is null."),
            _xsrfCookieOptions
        );
        return new StatusCodeResult(StatusCodes.Status200OK);
    }
}