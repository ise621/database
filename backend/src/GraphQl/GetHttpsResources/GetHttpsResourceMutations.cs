using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Database.Extensions;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Data;
using HotChocolate.Types;

namespace Database.GraphQl.GetHttpsResources
{
    [ExtendObjectType(nameof(Mutation))]
    public sealed class GetHttpsResourceMutations
    {
        [UseDbContext(typeof(Data.ApplicationDbContext))]
        // [UseUserManager]
        // [Authorize(Policy = Configuration.AuthConfiguration.WritePolicy)]
        public async Task<CreateGetHttpsResourcePayload> CreateGetHttpsResourceAsync(
            CreateGetHttpsResourceInput input,
            // [GlobalState(nameof(ClaimsPrincipal))] ClaimsPrincipal claimsPrincipal,
            // [ScopedService] UserManager<Data.User> userManager,
            Data.ApplicationDbContext context,
            [Service] AppSettings appSettings,
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
            {
                return new CreateGetHttpsResourcePayload(
                    new CreateGetHttpsResourceError(
                      CreateGetHttpsResourceErrorCode.UNAUTHORIZED,
                      $"The access token {input.AccessToken} is invalid.",
                      new[] { nameof(input), nameof(input.AccessToken).FirstCharToLower() }
                    )
                );
            }
            var getHttpsResource = new Data.GetHttpsResource(
                description: input.Description,
                hashValue: input.HashValue,
                dataFormatId: input.DataFormatId,
                calorimetricDataId: input.DataKind == Enumerations.DataKind.CALORIMETRIC_DATA ? input.DataId : null,
                hygrothermalDataId: input.DataKind == Enumerations.DataKind.HYGROTHERMAL_DATA ? input.DataId : null,
                opticalDataId: input.DataKind == Enumerations.DataKind.OPTICAL_DATA ? input.DataId : null,
                photovoltaicDataId: input.DataKind == Enumerations.DataKind.PHOTOVOLTAIC_DATA ? input.DataId : null,
                parentId: input.ParentId,
                archivedFilesMetaInformation: input.ArchivedFilesMetaInformation.Select(i =>
                    new Data.FileMetaInformation(
                        path: i.Path,
                        dataFormatId: i.DataFormatId
                    )
                ).ToList(),
                appliedConversionMethod:
                    input.AppliedConversionMethod is null
                    ? null
                    : new Data.ToTreeVertexAppliedConversionMethod(
                        methodId: input.AppliedConversionMethod.MethodId,
                        arguments: input.AppliedConversionMethod.Arguments.Select(a =>
                            new Data.NamedMethodArgument(
                                name: a.Name,
                                // TODO Turn `a.Value` into `JsonDocument`. It comes
                                // as nested `IReadOnlyDictionary/-List` as said on
                                // https://chillicream.com/docs/hotchocolate/v11/defining-a-schema/scalars/#any-type
                                // Take inspiration from
                                // https://josef.codes/custom-dictionary-string-object-jsonconverter-for-system-text-json/
                                // and
                                // https://github.com/joseftw/JOS.SystemTextJsonDictionaryStringObjectJsonConverter/blob/develop/src/JOS.SystemTextJsonDictionaryObjectModelBinder/DictionaryStringObjectJsonConverter.cs
                                // This is also needed in `OpticalDataMutations`.
                                value: JsonDocument.Parse(@"""TODO""")
                            )
                        ).ToList(),
                        sourceName: input.AppliedConversionMethod.SourceName
                    )
            );
            context.GetHttpsResources.Add(getHttpsResource);
            await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return new CreateGetHttpsResourcePayload(getHttpsResource);
        }
    }
}
