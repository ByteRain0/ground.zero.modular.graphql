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
        //TODO: track https://github.com/dotnet/aspire/issues/6852 for OTel setup.
        builder.AddNpgsqlDbContext<MangaDbContext>("default-db", c => c.DisableTracing = true);

        var serviceAssembly = typeof(ApplicationBuilderExtensions).Assembly;
        var contractAssembly = typeof(GetManga).Assembly;

        builder.Services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(serviceAssembly);
        });

        builder.Services.AddFluentValidation([serviceAssembly, contractAssembly]);

        builder.AddGraphQL().AddTypeModule<AuthorSettingsModule>(_ => new AuthorSettingsModule(builder.Configuration));

        return builder;
    }
}