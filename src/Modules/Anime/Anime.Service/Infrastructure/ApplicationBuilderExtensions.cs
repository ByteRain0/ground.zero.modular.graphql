using Anime.Contracts.Services.Anime.Commands;
using Anime.Service.Infrastructure.Data;
using MediatR.Extensions.FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Anime.Service.Infrastructure;

public static class ApplicationBuilderExtensions
{
    public static IHostApplicationBuilder AddAnimeServices(this IHostApplicationBuilder builder)
    {
        //TODO: uncomment once issue https://github.com/dotnet/aspire/issues/6852 is fixed
        //builder.AddNpgsqlDbContext<AnimeDbContext>("default-db");
        
        var connectionString = builder.Configuration.GetConnectionString("default-db");
        builder.Services.AddDbContext<AnimeDbContext>(
            opts => opts.UseNpgsql(connectionString));
        
        var serviceAssembly = typeof(ApplicationBuilderExtensions).Assembly;
        var contractAssembly = typeof(CreateAnime).Assembly;
        
        builder.Services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(serviceAssembly);
        });

        builder.Services.AddFluentValidation([serviceAssembly, contractAssembly]);

        builder
            .AddGraphQL()
            .AddAnimeServiceTypes();
        
        return builder;
    }
}