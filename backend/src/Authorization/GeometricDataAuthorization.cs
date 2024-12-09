using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Database.Authorization;

public static class GeometricDataAuthorization
{
    public static Task<bool> IsAuthorizedToCreateGeometricDataForInstitution(
        Guid institutionId,
        AppSettings appSettings,
        IHttpClientFactory httpClientFactory,
        IHttpContextAccessor httpContextAccessor,
        CancellationToken cancellationToken
    )
    {
        return CommonAuthorization.IsCurrentUserAtLeastAssistantOfVerifiedInstitution(institutionId, appSettings, httpClientFactory, httpContextAccessor, cancellationToken);
    }
}