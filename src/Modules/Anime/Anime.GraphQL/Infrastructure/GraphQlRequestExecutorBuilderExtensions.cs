using HotChocolate.Execution.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Anime.GraphQL.Infrastructure;

public static class GraphQlRequestExecutorBuilderExtensions
{
    public static IRequestExecutorBuilder AddAnimeGraphql(this IRequestExecutorBuilder builder)
    {
        builder.AddAnimeGraphqlTypes();
        return builder;
    }
}