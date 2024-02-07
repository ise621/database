using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Database.Extensions;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Data;
using HotChocolate.Types;

namespace Database.GraphQl.PhotovoltaicDataX
{
    [ExtendObjectType(nameof(Mutation))]
    public sealed class PhotovoltaicDataMutations
    {
        // [UseUserManager]
        // [Authorize(Policy = Configuration.AuthConfiguration.WritePolicy)]
        public async Task<CreatePhotovoltaicDataPayload> CreatePhotovoltaicDataAsync(
            CreatePhotovoltaicDataInput input,
            // ClaimsPrincipal claimsPrincipal,
            // [ScopedService] UserManager<Data.User> userManager,
            Data.ApplicationDbContext context,
            [Service] AppSettings appSettings,
            CancellationToken cancellationToken
        )
        {
            // if (!await PhotovoltaicDataAuthorization.IsAuthorizedToCreatePhotovoltaicDataForInstitution(
            //      claimsPrincipal,
            //      input.CreatorId,
            //      userManager,
            //      context,
            //      cancellationToken
            //      ).ConfigureAwait(false)
            // )
            if (input.AccessToken != appSettings.AccessToken)
            {
                return new CreatePhotovoltaicDataPayload(
                    new CreatePhotovoltaicDataError(
                      CreatePhotovoltaicDataErrorCode.UNAUTHORIZED,
                      $"The access token {input.AccessToken} is invalid.",
                      new[] { nameof(input), nameof(input.AccessToken).FirstCharToLower() }
                    )
                );
            }
            var photovoltaicData = new Data.PhotovoltaicData(
                locale: input.Locale,
                componentId: input.ComponentId,
                name: input.Name,
                description: input.Description,
                warnings: input.Warnings,
                creatorId: input.CreatorId,
                createdAt: input.CreatedAt,
                appliedMethod: new Data.AppliedMethod(
                    methodId: input.AppliedMethod.MethodId,
                    arguments: input.AppliedMethod.Arguments
                        .Select(a => new Data.NamedMethodArgument(
                            name: a.Name,
                            // TODO Turn `a.Value` into `JsonDocument`. It comes
                            // as nested `IReadOnlyDictionary/-List` as said on
                            // https://chillicream.com/docs/hotchocolate/v11/defining-a-schema/scalars/#any-type
                            // Take inspiration from
                            // https://josef.codes/custom-dictionary-string-object-jsonconverter-for-system-text-json/
                            // and
                            // https://github.com/joseftw/JOS.SystemTextJsonDictionaryStringObjectJsonConverter/blob/develop/src/JOS.SystemTextJsonDictionaryObjectModelBinder/DictionaryStringObjectJsonConverter.cs
                            // This is also needed in `GetHttpsResourceMutations`.
                            value: JsonDocument.Parse(@"""TODO""")
                        ))
                        .ToList(),
                    sources: input.AppliedMethod.Sources
                        .Select(s => new Data.NamedMethodSource(
                            name: s.Name,
                            value: new Data.CrossDatabaseDataReference(
                                dataId: s.Value.DataId,
                                dataTimestamp: s.Value.DataTimestamp,
                                dataKind: s.Value.DataKind,
                                databaseId: s.Value.DatabaseId
                            )
                        ))
                        .ToList()
                ),
                approvals: input.Approvals.Select(a =>
                    new Data.DataApproval(
                        timestamp: a.Timestamp,
                        signature: a.Signature,
                        keyFingerprint: a.KeyFingerprint,
                        query: a.Query,
                        response: a.Response,
                        approverId: a.ApproverId,
                        statement: a.Statement
                    )
                ).ToList()
            // approval: input.Approval
            );
            var resource = new Data.GetHttpsResource(
                    description: input.RootResource.Description,
                    hashValue: input.RootResource.HashValue,
                    dataFormatId: input.RootResource.DataFormatId,
                    parentId: null,
                    archivedFilesMetaInformation: input.RootResource.ArchivedFilesMetaInformation.Select(i =>
                        new Data.FileMetaInformation(
                            path: i.Path,
                            dataFormatId: i.DataFormatId
                        )
                    ).ToList(),
                    appliedConversionMethod:
                        input.RootResource.AppliedConversionMethod is null
                        ? null
                        : new Data.ToTreeVertexAppliedConversionMethod(
                            methodId: input.RootResource.AppliedConversionMethod.MethodId,
                            arguments: input.RootResource.AppliedConversionMethod.Arguments.Select(a =>
                                new Data.NamedMethodArgument(
                                    name: a.Name,
                                    // TODO Turn `a.Value` into `JsonDocument`. It comes
                                    // as nested `IReadOnlyDictionary/-List` as said on
                                    // https://chillicream.com/docs/hotchocolate/v11/defining-a-schema/scalars/#any-type
                                    // Take inspiration from
                                    // https://josef.codes/custom-dictionary-string-object-jsonconverter-for-system-text-json/
                                    // and
                                    // https://github.com/joseftw/JOS.SystemTextJsonDictionaryStringObjectJsonConverter/blob/develop/src/JOS.SystemTextJsonDictionaryObjectModelBinder/DictionaryStringObjectJsonConverter.cs
                                    // This is also needed in `GetHttpsResourceMutations`.
                                    value: JsonDocument.Parse(@"""TODO""")
                                )
                            ).ToList(),
                            sourceName: input.RootResource.AppliedConversionMethod.SourceName
                        )
            );
            photovoltaicData.Resources.Add(resource);
            context.PhotovoltaicData.Add(photovoltaicData);
            await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return new CreatePhotovoltaicDataPayload(photovoltaicData);
        }
    }
}
