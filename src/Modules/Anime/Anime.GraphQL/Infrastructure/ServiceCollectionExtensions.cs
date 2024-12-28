using Anime.Contracts.Exceptions;
using Anime.Contracts.Services.Anime.Telemetry;
using HotChocolate.Execution.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Anime.GraphQL.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IRequestExecutorBuilder AddAnimeGraphql(this IRequestExecutorBuilder builder)
    {
        builder.AddAnimeGraphqlTypes()
            // Example of how to enrich the error with some useful information for client.
            .AddErrorFilter(error =>
            {
                // These should be for low level details that we need to hide from our client.
                if (error.Exception is IAnimeBasedException ex)
                {
                    var errorBuilder = ErrorBuilder.FromError(error);
                    errorBuilder.SetExtension(AnimeTelemetryTags.AnimeIdOrTitle, ex.IdOrTitle);
                    
                    return errorBuilder.Build();
                }
                
                // TODO: filter out low level exceptions.
                
                return error;
            });
        return builder;
    }
}