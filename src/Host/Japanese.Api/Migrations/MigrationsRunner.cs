using System.Diagnostics;
using Anime.Service.Infrastructure.Data;
using Core.Otel;
using Core.Otel.Sources;
using Manga.Service.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Japanese.Api.Migrations;

internal static class MigrationsRunner
{
    internal static void ApplyAnimeMigrations(this IApplicationBuilder app)
    {
        using var applyMigrationsActivity = JapaneseApiRunTimeDiagnosticConfig.Source.StartActivity();
        Activity.Current?.SetTag("DbContextType", typeof(AnimeDbContext));
        
        using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();

        var logger = serviceScope.ServiceProvider.GetService<ILogger<Program>>();
        logger?.LogInformation(
            "Applying migrations for {DbContextType} {TraceId}", 
            typeof(AnimeDbContext), applyMigrationsActivity?.TraceId);
        
        using var context = serviceScope.ServiceProvider.GetService<AnimeDbContext>();
        context?.Database.Migrate();
    }
    
    internal static void ApplyMangaMigrations(this IApplicationBuilder app)
    {
        using var applyMigrationsActivity = JapaneseApiRunTimeDiagnosticConfig.Source.StartActivity();
        Activity.Current?.SetTag("DbContextType", typeof(MangaDbContext));
        using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
        
        var logger = serviceScope.ServiceProvider.GetService<ILogger<Program>>();
        logger?.LogInformation(
            "Applying migrations for {DbContextType} {TraceId}",
            typeof(AnimeDbContext), applyMigrationsActivity?.TraceId);
        
        using var context = serviceScope.ServiceProvider.GetService<MangaDbContext>();
        context?.Database.Migrate();
    }
    
}