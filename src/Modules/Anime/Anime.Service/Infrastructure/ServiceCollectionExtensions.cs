using Anime.Contracts.Services.Anime.Commands;
using Anime.Service.Infrastructure.Data;
using MediatR.Extensions.FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Anime.Service.Infrastructure;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Install all dependencies of the Anime Module.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddAnime(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Anime");
        
        services.AddDbContext<AnimeDbContext>(o => o.UseNpgsql(connectionString));

        var serviceAssembly = typeof(ServiceCollectionExtensions).Assembly;
        var contractAssembly = typeof(CreateAnime).Assembly;
        
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(serviceAssembly);
        });

        services.AddFluentValidation([serviceAssembly, contractAssembly]);

        services
            .AddGraphQL()
            .AddAnimeServiceTypes();
        
        return services;
    }
}