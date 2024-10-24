using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Database.Data;
using Database.Enumerations;
using Database.Extensions;
using HotChocolate;
using HotChocolate.Types;

namespace Database.GraphQl.GetHttpsResources;

[ExtendObjectType(nameof(Mutation))]
public sealed class GetHttpsResourceMutations
{
    // [UseUserManager]
    // [Authorize(Policy = Configuration.AuthConfiguration.WritePolicy)]
    public async Task<CreateGetHttpsResourcePayload> CreateGetHttpsResourceAsync(
        CreateGetHttpsResourceInput input,
        // ClaimsPrincipal claimsPrincipal,
        // [ScopedService] UserManager<Data.User> userManager,
        ApplicationDbContext context,
        AppSettings appSettings,
        CancellationToken cancellationToken
    )
    {
        // if (!await GetHttpsResourceAuthorization.IsAuthorizedToCreateGetHttpsResourceForInstitution(
        //      claimsPrincipal,
        //      input.CreatorId,
        //      userManager,
        //      context,
        //      cancellationToken
        //      ).ConfigureAwait(false)
        // )
        if (input.AccessToken != appSettings.AccessToken)
            return new CreateGetHttpsResourcePayload(
                new CreateGetHttpsResourceError(
                    CreateGetHttpsResourceErrorCode.UNAUTHORIZED,
                    $"The access token {input.AccessToken} is invalid.",
                    new[] { nameof(input), nameof(input.AccessToken).FirstCharToLower() }
                )
            );
        var getHttpsResource = new GetHttpsResource(
            input.Description,
            input.HashValue,
            input.DataFormatId,
            input.DataKind == DataKind.CALORIMETRIC_DATA ? input.DataId : null,
            input.DataKind == DataKind.HYGROTHERMAL_DATA ? input.DataId : null,
            input.DataKind == DataKind.OPTICAL_DATA ? input.DataId : null,
            input.DataKind == DataKind.PHOTOVOLTAIC_DATA ? input.DataId : null,
            input.DataKind == DataKind.GEOMETRIC_DATA ? input.DataId : null,
            input.ParentId,
            input.ArchivedFilesMetaInformation.Select(i =>
                new FileMetaInformation(
                    i.Path,
                    i.DataFormatId
                )
            ).ToList(),
            input.AppliedConversionMethod is null
                ? null
                : new ToTreeVertexAppliedConversionMethod(
                    input.AppliedConversionMethod.MethodId,
                    input.AppliedConversionMethod.Arguments.Select(a =>
                        new NamedMethodArgument(
                            a.Name,
                            // TODO Turn `a.Value` into `JsonDocument`. It comes
                            // as nested `IReadOnlyDictionary/-List` as said on
                            // https://chillicream.com/docs/hotchocolate/v11/defining-a-schema/scalars/#any-type
                            // Take inspiration from
                            // https://josef.codes/custom-dictionary-string-object-jsonconverter-for-system-text-json/
                            // and
                            // https://github.com/joseftw/JOS.SystemTextJsonDictionaryStringObjectJsonConverter/blob/develop/src/JOS.SystemTextJsonDictionaryObjectModelBinder/DictionaryStringObjectJsonConverter.cs
                            // This is also needed in `OpticalDataMutations`.
                            JsonDocument.Parse(@"""TODO""")
                        )
                    ).ToList(),
                    input.AppliedConversionMethod.SourceName
                )
        );
        context.GetHttpsResources.Add(getHttpsResource);
        await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return new CreateGetHttpsResourcePayload(getHttpsResource);
    }
}