using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Database.Data;
using Database.Filters;
using Database.Utilities;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;

namespace Database.Controllers;

public static partial class Log
{
    [LoggerMessage(
        EventId = 0,
        Level = LogLevel.Information,
        Message =
            "Uploaded file '{TrustedFileNameForDisplay}' saved to '{TargetDirectoryPath}' as '{TrustedFileNameForFileStorage}'")]
    public static partial void SavedUploadedFile(
        this ILogger logger,
        string? trustedFileNameForDisplay, string targetDirectoryPath,
        string trustedFileNameForFileStorage
    );
}

// Inspired by https://docs.microsoft.com/en-us/aspnet/core/mvc/models/file-uploads?view=aspnetcore-5.0#upload-large-files-with-streaming
// and https://github.com/dotnet/AspNetCore.Docs/blob/b4599432690b8753fc2eac23d52957f47e01997a/aspnetcore/mvc/models/file-uploads/samples/3.x/SampleApp/
public class FileUploadController : Controller
{
    private const long
        _fileSizeLimit =
            10737418240; // 10 GiB = 10 * 1024 MiB = 10 * 1024 * 1024^2 Byte = 10 * 1024 * 1048576 Byte = 10737418240 Byte

    private const string _targetDirectoryPath = "./files/";

    // Get the default form options so that we can use them to set the default 
    // limits for request body data.
    private static readonly FormOptions _defaultFormOptions = new();
    private readonly string _accessToken;
    private readonly ApplicationDbContext _context;
    private readonly ILogger<FileUploadController> _logger;

    private readonly string[] _permittedExtensions =
        { ".json", ".xml", ".txt", ".csv", ".ifc", ".rad", ".svg", ".pdf", ".png" };

    public FileUploadController(
        ILogger<FileUploadController> logger,
        ApplicationDbContext context,
        AppSettings appSettings
    )
    {
        _logger = logger;
        _context = context;
        _accessToken = appSettings.AccessToken;
        // To save physical files to the temporary files folder, use:
        //_targetDirectoryPath = Path.GetTempPath();
    }

    // The following upload methods:
    //
    // 1. Disable the form value model binding to take control of handling 
    //    potentially large files.
    //
    // 2. Typically, antiforgery tokens are sent in request body. Since we 
    //    don't want to read the request body early, the tokens are sent via 
    //    headers. The antiforgery token filter first looks for tokens in 
    //    the request header and then falls back to reading the body.

    [HttpPost("~/api/upload-file")]
    [DisableFormValueModelBinding]
    // TODO Add this `[ValidateAntiForgeryToken]` once we know where to set the generation token cookie!
    // TODO Where to put: [GenerateAntiforgeryTokenCookie] ?
    // TODO Are both RequestFormLimits and RequestSizeLimit needed?
    // See https://docs.microsoft.com/en-us/aspnet/core/mvc/models/file-uploads?view=aspnetcore-5.0#multipart-body-length-limit
    [RequestFormLimits(MultipartBodyLengthLimit = 10737418240)] // 10 GiB
    // See https://docs.microsoft.com/en-us/aspnet/core/mvc/models/file-uploads?view=aspnetcore-5.0#kestrel-maximum-request-body-size
    [RequestSizeLimit(10737418240)] // 10 GiB
    public async Task<IActionResult> UploadFile(
        [FromQuery] Guid getHttpsResourceUuid,
        CancellationToken cancellationToken
    )
    {
        if (!MultipartRequestHelper.IsMultipartContentType(Request.ContentType))
        {
            ModelState.AddModelError("File",
                "The request content is not multipart.");
            return BadRequest(ModelState);
        }

        if (!await _context.GetHttpsResources.AsNoTracking()
                .Where(u => u.Id == getHttpsResourceUuid)
                .AnyAsync(cancellationToken)
                .ConfigureAwait(false)
           )
        {
            ModelState.AddModelError("GetHttpsResourceUuid",
                $"There is no GET HTTPS resource with UUID {getHttpsResourceUuid:D}.");
            return BadRequest(ModelState);
        }

        var boundary = MultipartRequestHelper.GetBoundary(
            MediaTypeHeaderValue.Parse(Request.ContentType),
            _defaultFormOptions.MultipartBoundaryLengthLimit);
        var reader = new MultipartReader(boundary, HttpContext.Request.Body);
        var section = await reader.ReadNextSectionAsync(cancellationToken).ConfigureAwait(false);

        if (section is null)
        {
            ModelState.AddModelError("AccessToken", "Cannot read access token.");
            return BadRequest(ModelState);
        }

        var hasFirstContentDispositionHeader =
            ContentDispositionHeaderValue.TryParse(
                section.ContentDisposition, out var firstContentDisposition);
        if (!hasFirstContentDispositionHeader)
        {
            ModelState.AddModelError("AccessToken", "Cannot parse content disposition header value.");
            return BadRequest(ModelState);
        }

        if (!firstContentDisposition?.Name.Equals("accessToken") ??
            throw new ArgumentException("Impossible (because `hasFirstContentDispositionHeader` is `true`)"))
        {
            ModelState.AddModelError("AccessToken",
                $"Access token must come first, got {firstContentDisposition?.Name} instead.");
            return BadRequest(ModelState);
        }

        var accessToken = await new StreamReader(section.Body).ReadToEndAsync(cancellationToken).ConfigureAwait(false);
        if (accessToken != _accessToken)
        {
            ModelState.AddModelError("AccessToken", $"The access token {accessToken} is invalid.");
            return BadRequest(ModelState);
        }

        Directory.CreateDirectory(_targetDirectoryPath);

        section = await reader.ReadNextSectionAsync(cancellationToken).ConfigureAwait(false);
        while (section != null)
        {
            var hasContentDispositionHeader =
                ContentDispositionHeaderValue.TryParse(
                    section.ContentDisposition, out var contentDisposition);

            if (hasContentDispositionHeader)
            {
                // This check assumes that there's a file
                // present without form data. If form data
                // is present, this method immediately fails
                // and returns the model error.
                if (!MultipartRequestHelper
                        .HasFileContentDisposition(contentDisposition ??
                                                   throw new ArgumentException(
                                                       "Impossible (because `hasContentDispositionHeader` is `true`)")))
                {
                    ModelState.AddModelError("File",
                        "There is no content-disposition header.");
                    return BadRequest(ModelState);
                }

                // Don't trust the file name sent by the client. To display
                // the file name, HTML-encode the value.
                var trustedFileNameForDisplay = WebUtility.HtmlEncode(
                    contentDisposition.FileName.Value);
                var trustedFileNameForFileStorage = getHttpsResourceUuid.ToString("D");

                // **WARNING!**
                // In the following example, the file is saved without
                // scanning the file's contents. In most production
                // scenarios, an anti-virus/anti-malware scanner API
                // is used on the file before making the file available
                // for download or for use by other systems. 
                // For more information, see the topic that accompanies 
                // this sample.

                var streamedFileContent = await FileHelpers.ProcessStreamedFile(
                    section,
                    contentDisposition,
                    ModelState,
                    _permittedExtensions,
                    _fileSizeLimit
                ).ConfigureAwait(false);

                if (!ModelState.IsValid) return BadRequest(ModelState);

                using var targetStream = System.IO.File.Create(
                    Path.Combine(_targetDirectoryPath, trustedFileNameForFileStorage));
                await targetStream.WriteAsync(streamedFileContent, cancellationToken).ConfigureAwait(false);

                _logger.SavedUploadedFile(trustedFileNameForDisplay, _targetDirectoryPath,
                    trustedFileNameForFileStorage);
            }

            // Drain any remaining section body that hasn't been consumed and
            // read the headers for the next section.
            section = await reader.ReadNextSectionAsync(cancellationToken).ConfigureAwait(false);
        }

        return Created(nameof(FileUploadController), null);
    }

    private static Encoding GetEncoding(MultipartSection section)
    {
        var hasMediaTypeHeader =
            MediaTypeHeaderValue.TryParse(section.ContentType, out var mediaType);

        // UTF-7 is insecure and shouldn't be honored. UTF-8 succeeds in 
        // most cases.
#pragma warning disable SYSLIB0001
        if (!hasMediaTypeHeader || Encoding.UTF7.Equals(
                mediaType?.Encoding ??
                throw new ArgumentException("Impossible (because `hasMediaTypeHeader` is `true`)!"))
           )
#pragma warning restore SYSLIB0001
            return Encoding.UTF8;

        return mediaType.Encoding;
    }
}