using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Database.Authorization;

public static class FileUploadDataAuthorization
{
    public static Task<bool> IsAuthorizedToUploadFilesForInstitution(
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