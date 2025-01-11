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

    public static IRequestExecutorBuilder AddGraphQLInfrastructure(
        this IServiceCollection services) =>
        services
            .AddGraphQLServer()
            .ModifyOptions(options =>
            {
                options.DefaultBindingBehavior = BindingBehavior.Explicit;
                options.EnsureAllNodesCanBeResolved = true;
            })
            .AddAnimeGraphqlTypes()
            .AddMangaGraphqlTypes()
            .AddGraphQlConventions();

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
            path:"appsettings.DynamicEntities.json", 
            optional: false, 
            reloadOnChange: true);

        return builder;
    }
    
    private static IRequestExecutorBuilder AddGraphQlConventions(
        this IRequestExecutorBuilder builder)
    {
        builder
            .AddProjections()
            .AddFiltering(cfg =>
            {
                cfg.AddDefaults().BindRuntimeType<string, RestrictedStringOperationFilterInputType>();
            })
            .AddSorting()
            .AddPagingArguments()
            .AddGlobalObjectIdentification()
            .AddQueryConventions()
            .AddMutationConventions()
            .AddInMemorySubscriptions()
            .AddErrorFilter<GraphQLAuthExceptionFilter>()
            .AddErrorFilter<BusinessValidationErrorFilter>()
            .AddFairyBread(opts => { opts.ThrowIfNoValidatorsFound = true; })
            .InitializeOnStartup()
            .AddInstrumentation();

        builder.ModifyPagingOptions(cfg =>
        {
            cfg.DefaultPageSize = 10;
            cfg.MaxPageSize = 50;
        });

        if (AppHost.IsDevelopment())
        {
            builder.ModifyRequestOptions(cfg =>
            {
                //Modify responses in case you need extensive error explanation on dev env.
                cfg.IncludeExceptionDetails = true;
            });
        }

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