using Manga.Contracts.Services.Manga.Queries;
using Manga.Service.Infrastructure.Data;
using Manga.Service.Infrastructure.Data.DynamicTypes.AuthorSettings;
using MediatR.Extensions.FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Manga.Service.Infrastructure;

public static class ApplicationBuilderExtensions
{
    public static IHostApplicationBuilder AddMangaServices(this IHostApplicationBuilder builder)
    {
        //TODO: uncomment once issue https://github.com/dotnet/aspire/issues/6852 is fixed
        builder.AddNpgsqlDbContext<MangaDbContext>("default-db", c => c.DisableTracing = true);

        // var connectionString = builder.Configuration.GetConnectionString("default-db");
        // builder.Services.AddDbContext<MangaDbContext>(
        //     opts => opts.UseNpgsql(connectionString));
        
        var serviceAssembly = typeof(ApplicationBuilderExtensions).Assembly;
        var contractAssembly = typeof(GetManga).Assembly;

        builder.Services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(serviceAssembly);
        });

        builder.Services.AddFluentValidation([serviceAssembly, contractAssembly]);
        
        builder.AddGraphQL()
            .AddTypeModule<AuthorSettingsModule>(_ => new AuthorSettingsModule(builder.Configuration));
        
        return builder;
    }
}