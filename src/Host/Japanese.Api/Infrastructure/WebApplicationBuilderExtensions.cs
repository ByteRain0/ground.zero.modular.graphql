using Anime.Service.Infrastructure;
using Core.Auth;
using Core.Behaviors;
using Core.Environment;
using Core.Otel.Sources;
using Core.QueryFilters;
using Core.Validation;
using HotChocolate.Data;
using HotChocolate.Execution.Configuration;
using Manga.Service.Infrastructure;
using MediatR;

namespace Japanese.Api.Infrastructure;

public static class WebApplicationBuilderExtensions
{
    public static IHostApplicationBuilder AddApplicationServices(
        this IHostApplicationBuilder builder)
    {
        builder.Services.AddOpenTelemetry()
            .WithTracing(tracing => tracing.AddSource(JapaneseApiRunTimeDiagnosticConfig.Source.Name))
            .WithMetrics(metrics => metrics.AddMeter(JapaneseApiRunTimeDiagnosticConfig.Meter.Name));

        builder
            .AddAnimeServices()
            .AddMangaServices();

        builder.Services
            .AddMediatRBehaviorPipeline()
            .AddHttpContextAccessor()
            .AddSingleton(TimeProvider.System);

        return builder;
    }

    public static IServiceCollection AddKeyCloakBasedAuth(this IServiceCollection services)
    {
        services.AddAuthorization();
        services.AddAuthentication()
            .AddKeycloakJwtBearer(
                serviceName: "keycloak",
                realm: "japanese-culture",
                configureOptions: bearerOptions =>
                {
                    if (AppHost.IsDevelopment()) bearerOptions.RequireHttpsMetadata = false;
                    bearerOptions.Audience = "account";
                });

        return services;
    }

    public static WebApplicationBuilder AddDynamicEntitiesConfigurations(this WebApplicationBuilder builder)
    {
        builder.Configuration.AddJsonFile(
            path: "appsettings.DynamicEntities.json",
            optional: false,
            reloadOnChange: true);

        return builder;
    }

    public static IRequestExecutorBuilder AddGraphQLInfrastructure(
        this IServiceCollection services)
    {
        var builder = services.AddGraphQLServer();

        // Add default settings
        builder
            .AddInstrumentation()
            .InitializeOnStartup()
            .ModifyOptions(options =>
            {
                options.DefaultBindingBehavior = BindingBehavior.Explicit;
                options.EnsureAllNodesCanBeResolved = true;
            })
            .ModifyRequestOptions(cfg =>
            {
                //Modify responses in case you need extensive error explanation on dev env.
                cfg.IncludeExceptionDetails = AppHost.IsDevelopment();
            });
        
        // Add paging|filtering|sorting|projections
        builder
            .AddQueryContext()
            .AddProjections()
            .AddFiltering(cfg =>
            {
                cfg.AddDefaults().BindRuntimeType<string, RestrictedStringOperationFilterInputType>();
            })
            .AddSorting()
            .AddPagingArguments()
            .ModifyPagingOptions(cfg =>
            {
                cfg.DefaultPageSize = 10;
                cfg.MaxPageSize = 50;
            });

        // Add usage patterns
        builder
            .AddGlobalObjectIdentification()
            .AddQueryConventions()
            .AddMutationConventions()
            .AddInMemorySubscriptions()
            .AddFairyBread(opts => { opts.ThrowIfNoValidatorsFound = true; });

        // Add error filters
        builder
            .AddErrorFilter<GraphQLAuthExceptionFilter>()
            .AddErrorFilter<BusinessValidationErrorFilter>();
        
        // Add custom types
        builder
            .AddAnimeGraphqlTypes()
            .AddMangaGraphqlTypes();
        
        return builder;
    }

    private static IServiceCollection AddMediatRBehaviorPipeline(
        this IServiceCollection services)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ActivityTracingBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
        return services;
    }
}