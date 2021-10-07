using System;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Data.Filters;
using HotChocolate.Types.Pagination;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using OpenIddict.Validation.AspNetCore;
using IServiceCollection = Microsoft.Extensions.DependencyInjection.IServiceCollection;
using Microsoft.Extensions.Logging;

namespace Database.Configuration
{
    public static class GraphQlConfiguration
    {
        public static void ConfigureServices(
            IServiceCollection services,
            IWebHostEnvironment environment
            )
        {
            services.AddGraphQLServer()
            // Types
            .AddType<GraphQl.Common.OpenEndedDateTimeRangeType>()
            // Extensions
            .AddProjections()
            .AddFiltering()
            .AddSorting()
            .AddAuthorization()
            .AddApolloTracing(
                HotChocolate.Execution.Options.TracingPreference.OnDemand
            ) // TODO Do we want or need this?
            .AddGlobalObjectIdentification()
            .AddQueryFieldToMutationPayloads()
            .ModifyOptions(options =>
              {
                  // https://github.com/ChilliCream/hotchocolate/blob/main/src/HotChocolate/Core/src/Types/Configuration/Contracts/ISchemaOptions.cs
                  options.StrictValidation = true;
                  options.UseXmlDocumentation = false;
                  options.SortFieldsByName = true;
                  options.RemoveUnreachableTypes = false;
                  options.DefaultBindingBehavior = HotChocolate.Types.BindingBehavior.Implicit;
                  /* options.FieldMiddleware = ... */
              }
              )
            .ModifyRequestOptions(options =>
                {
                    // https://github.com/ChilliCream/hotchocolate/blob/main/src/HotChocolate/Core/src/Execution/Options/RequestExecutorOptions.cs
                    /* options.ExecutionTimeout = ...; */
                    options.IncludeExceptionDetails = environment.IsDevelopment(); // Default is `Debugger.IsAttached`.
                    /* options.QueryCacheSize = ...; */
                    /* options.UseComplexityMultipliers = ...; */
                }
                )
                  // TODO Configure `https://github.com/ChilliCream/hotchocolate/blob/main/src/HotChocolate/Core/src/Validation/Options/ValidationOptions.cs`. But how?
                  // Subscriptions
                  /* .AddInMemorySubscriptions() */
                  // TODO Persisted queries
                  /* .AddFileSystemQueryStorage("./persisted_queries") */
                  /* .UsePersistedQueryPipeline(); */
                  /* TODO services.AddDiagnosticObserver<GraphQl.DiagnosticObserver>(); */
                  .AddHttpRequestInterceptor(async (httpContext, requestExecutor, requestBuilder, cancellationToken) =>
                  {
                      // HotChocolate uses the default cookie authentication
                      // scheme `IdentityConstants.ApplicationScheme` by
                      // default. We want it to use the JavaScript Web Token
                      // (JWT), aka, Access Token, provided as `Authorization`
                      // HTTP header with the prefix `Bearer` as issued by
                      // OpenIddict though. This Access Token includes Scopes
                      // and Claims.
                      var authenticateResult = await httpContext.AuthenticateAsync(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme).ConfigureAwait(false);
                      if (authenticateResult.Succeeded && authenticateResult.Principal is not null)
                      {
                          httpContext.User = authenticateResult.Principal;
                      }
                  })
                  .AddDiagnosticEventListener(_ =>
                      new GraphQl.LoggingDiagnosticEventListener(
                          _.GetApplicationService<ILogger<GraphQl.LoggingDiagnosticEventListener>>()
                      )
                  )
                  .AddQueryType(d => d.Name(nameof(GraphQl.Query)))
                      .AddType<GraphQl.GetHttpsResources.GetHttpsResourceQueries>()
                      .AddType<GraphQl.OpticalDataX.OpticalDataQueries>()
                  .AddMutationType(d => d.Name(nameof(GraphQl.Mutation)))
                      .AddType<GraphQl.GetHttpsResources.GetHttpsResourceMutations>()
                      .AddType<GraphQl.OpticalDataX.OpticalDataMutations>()
                  /* .AddSubscriptionType(d => d.Name(nameof(GraphQl.Subscription))) */
                  /*     .AddType<ComponentSubscriptions>() */
                  // Scalar Types
                  .AddType(new UuidType('D')) // https://chillicream.com/docs/hotchocolate/defining-a-schema/scalars#uuid-type
                                              // Object Types
                    .AddType<GraphQl.GetHttpsResources.GetHttpsResourceType>()
                    .AddType<GraphQl.OpticalDataX.OpticalDataType>()
                    .AddType<GraphQl.NamedMethodArgumentType>()
                  /* .AddType<GraphQl.DataX.DataApproval>() */
                  /* .AddType<GraphQl.DataX.GetHttpsResourceTreeNonRootVertex>() */
                  /* .AddType<GraphQl.DataX.GetHttpsResourceTreeRoot>() */
                  /* .AddType<GraphQl.DataX.HygrothermalData>() */
                  /* .AddType<GraphQl.DataX.OpticalData>() */
                  /* .AddType<GraphQl.DataX.PhotovoltaicData>() */
                  /* .AddType<GraphQl.DataX.ResponseApproval>() */
                  // Data Loaders
                  /* .AddDataLoader<GraphQl.Components.ComponentByIdDataLoader>() */
                  // Filtering
                  .AddConvention<IFilterConvention>(
                   new FilterConventionExtension(descriptor =>
                     {
                         descriptor.Operation(DefaultFilterOperations.Equals).Name("equalTo");
                         descriptor.Operation(DefaultFilterOperations.NotEquals).Name("notEqualTo");
                         descriptor.Operation(DefaultFilterOperations.GreaterThan).Name("greaterThan");
                         descriptor.Operation(DefaultFilterOperations.NotGreaterThan).Name("notGreaterThan");
                         descriptor.Operation(DefaultFilterOperations.GreaterThanOrEquals).Name("greaterThanOrEqualTo");
                         descriptor.Operation(DefaultFilterOperations.NotGreaterThanOrEquals).Name("notGreaterThanOrEqualTo");
                         descriptor.Operation(DefaultFilterOperations.LowerThan).Name("lessThan");
                         descriptor.Operation(DefaultFilterOperations.NotLowerThan).Name("lessThan");
                         descriptor.Operation(DefaultFilterOperations.LowerThanOrEquals).Name("notLessThanOrEqualTo");
                         descriptor.Operation(DefaultFilterOperations.NotLowerThanOrEquals).Name("notLessThanOrEqualTo");
                         // TODO `inClosedInterval`
                         descriptor.Configure<ComparableOperationFilterInputType<double>>(x => x.Name("FloatPropositionInput"));
                         descriptor.Configure<ComparableOperationFilterInputType<Guid>>(x => x.Name("UuidPropositionInput"));
                         descriptor.BindRuntimeType<Data.GetHttpsResource, GraphQl.GetHttpsResources.GetHttpsResourceFilterType>();
                         descriptor.BindRuntimeType<Data.OpticalData, GraphQl.OpticalDataX.OpticalDataFilterType>();
                     }
                     )
                   )
                  // Paging
                  .SetPagingOptions(
                      new PagingOptions
                      {
                          MaxPageSize = int.MaxValue,
                          DefaultPageSize = int.MaxValue,
                          IncludeTotalCount = true
                      }
                  );
        }
    }
}