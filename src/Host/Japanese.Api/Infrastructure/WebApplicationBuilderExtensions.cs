using Anime.GraphQL.Infrastructure;
using Anime.Service.Infrastructure;
using Core.Auth;
using Core.Behaviors;
using Core.Environment;
using Core.QueryFilters;
using Core.Validation;
using HotChocolate.Data;
using HotChocolate.Execution.Configuration;
using Manga.GraphQL.Infrastructure;
using Manga.Service.Infrastructure;
using MediatR;

namespace Japanese.Api.Infrastructure;

public static class WebApplicationBuilderExtensions
{
    public static IHostApplicationBuilder AddApplicationServices(
        this IHostApplicationBuilder builder)
    {
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
        this IServiceCollection services)
    {
        return services
            .AddGraphQLServer()
            .ModifyOptions(options =>
            {
                options.DefaultBindingBehavior = BindingBehavior.Explicit;
                options.EnsureAllNodesCanBeResolved = true;
            })
            .AddAnimeGraphql()
            .AddMangaGraphql()
            .AddGraphQlConventions();
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
            .AddFairyBread(opts => { opts.ThrowIfNoValidatorsFound = true; })
            .AddErrorFilter<GraphQLAuthExceptionFilter>()
            .AddErrorFilter<BusinessValidationErrorFilter>()
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