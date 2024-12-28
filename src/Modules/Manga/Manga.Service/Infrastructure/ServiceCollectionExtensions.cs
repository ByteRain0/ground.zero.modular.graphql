using Manga.Contracts.Services.Manga.Queries;
using Manga.Service.Infrastructure.Data;
using MediatR.Extensions.FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Manga.Service.Infrastructure;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Install all dependencies of the Anime Module.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddManga(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Manga");
        
        services.AddDbContext<MangaDbContext>(o => o.UseNpgsql(connectionString));

        var serviceAssembly = typeof(ServiceCollectionExtensions).Assembly;
        var contractAssembly = typeof(GetManga).Assembly;
        
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(serviceAssembly);
        });

        services.AddFluentValidation([serviceAssembly, contractAssembly]);

        services
            .AddGraphQL();
        
        return services;
    }
}