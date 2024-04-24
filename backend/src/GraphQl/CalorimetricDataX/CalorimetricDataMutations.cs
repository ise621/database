using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Database.Data;
using Database.Extensions;
using HotChocolate;
using HotChocolate.Types;

namespace Database.GraphQl.CalorimetricDataX;

[ExtendObjectType(nameof(Mutation))]
public sealed class CalorimetricDataMutations
{
    // [UseUserManager]
    // [Authorize(Policy = Configuration.AuthConfiguration.WritePolicy)]
    public async Task<CreateCalorimetricDataPayload> CreateCalorimetricDataAsync(
        CreateCalorimetricDataInput input,
        // ClaimsPrincipal claimsPrincipal,
        // [ScopedService] UserManager<Data.User> userManager,
        ApplicationDbContext context,
        [Service] AppSettings appSettings,
        CancellationToken cancellationToken
    )
    {
        // if (!await CalorimetricDataAuthorization.IsAuthorizedToCreateCalorimetricDataForInstitution(
        //      claimsPrincipal,
        //      input.CreatorId,
        //      userManager,
        //      context,
        //      cancellationToken
        //      ).ConfigureAwait(false)
        // )
        if (input.AccessToken != appSettings.AccessToken)
            return new CreateCalorimetricDataPayload(
                new CreateCalorimetricDataError(
                    CreateCalorimetricDataErrorCode.UNAUTHORIZED,
                    $"The access token {input.AccessToken} is invalid.",
                    new[] { nameof(input), nameof(input.AccessToken).FirstCharToLower() }
                )
            );
        var calorimetricData = new CalorimetricData(
            input.Locale,
            input.ComponentId,
            input.Name,
            input.Description,
            input.Warnings,
            input.CreatorId,
            input.CreatedAt,
            new AppliedMethod(
                input.AppliedMethod.MethodId,
                input.AppliedMethod.Arguments
                    .Select(a => new NamedMethodArgument(
                        a.Name,
                        // TODO Turn `a.Value` into `JsonDocument`. It comes
                        // as nested `IReadOnlyDictionary/-List` as said on
                        // https://chillicream.com/docs/hotchocolate/v11/defining-a-schema/scalars/#any-type
                        // Take inspiration from
                        // https://josef.codes/custom-dictionary-string-object-jsonconverter-for-system-text-json/
                        // and
                        // https://github.com/joseftw/JOS.SystemTextJsonDictionaryStringObjectJsonConverter/blob/develop/src/JOS.SystemTextJsonDictionaryObjectModelBinder/DictionaryStringObjectJsonConverter.cs
                        // This is also needed in `GetHttpsResourceMutations`.
                        JsonDocument.Parse(@"""TODO""")
                    ))
                    .ToList(),
                input.AppliedMethod.Sources
                    .Select(s => new NamedMethodSource(
                        s.Name,
                        new CrossDatabaseDataReference(
                            s.Value.DataId,
                            s.Value.DataTimestamp,
                            s.Value.DataKind,
                            s.Value.DatabaseId
                        )
                    ))
                    .ToList()
            ),
            input.Approvals.Select(a =>
                new DataApproval(
                    a.Timestamp,
                    a.Signature,
                    a.KeyFingerprint,
                    a.Query,
                    a.Response,
                    a.ApproverId
                )
                {
                    Publication = a.Publication is null
                        ? null
                        : new Publication(
                            a.Publication.Title,
                            a.Publication.Abstract,
                            a.Publication.Section,
                            a.Publication.Authors,
                            a.Publication.Doi,
                            a.Publication.ArXiv,
                            a.Publication.Urn,
                            a.Publication.WebAddress
                        ),
                    Standard = a.Standard is null
                        ? null
                        : new Standard(
                            a.Standard.Title,
                            a.Standard.Abstract,
                            a.Standard.Section,
                            a.Standard.Year,
                            a.Standard.Standardizers,
                            a.Standard.Locator
                        )
                }
            ).ToList(),
            // approval: input.Approval
            input.GValues,
            input.UValues
        );
        var resource = new GetHttpsResource(
            input.RootResource.Description,
            input.RootResource.HashValue,
            input.RootResource.DataFormatId,
            null,
            input.RootResource.ArchivedFilesMetaInformation.Select(i =>
                new FileMetaInformation(
                    i.Path,
                    i.DataFormatId
                )
            ).ToList(),
            input.RootResource.AppliedConversionMethod is null
                ? null
                : new ToTreeVertexAppliedConversionMethod(
                    input.RootResource.AppliedConversionMethod.MethodId,
                    input.RootResource.AppliedConversionMethod.Arguments.Select(a =>
                        new NamedMethodArgument(
                            a.Name,
                            // TODO Turn `a.Value` into `JsonDocument`. It comes
                            // as nested `IReadOnlyDictionary/-List` as said on
                            // https://chillicream.com/docs/hotchocolate/v11/defining-a-schema/scalars/#any-type
                            // Take inspiration from
                            // https://josef.codes/custom-dictionary-string-object-jsonconverter-for-system-text-json/
                            // and
                            // https://github.com/joseftw/JOS.SystemTextJsonDictionaryStringObjectJsonConverter/blob/develop/src/JOS.SystemTextJsonDictionaryObjectModelBinder/DictionaryStringObjectJsonConverter.cs
                            // This is also needed in `GetHttpsResourceMutations`.
                            JsonDocument.Parse(@"""TODO""")
                        )
                    ).ToList(),
                    input.RootResource.AppliedConversionMethod.SourceName
                )
        );
        calorimetricData.Resources.Add(resource);
        context.CalorimetricData.Add(calorimetricData);
        await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return new CreateCalorimetricDataPayload(calorimetricData);
    }
}