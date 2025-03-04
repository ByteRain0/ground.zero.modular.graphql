using System.Diagnostics;
using Anime.Service.Infrastructure.Data;
using Core.Otel;
using Manga.Service.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Japanese.Api.MigrationService.Workers;

public class DbMigrator(
    IServiceProvider serviceProvider,
    IHostApplicationLifetime hostApplicationLifetime) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<DbMigrator>>();

        logger.LogInformation("Migrating anime database ...");
        using var animeMigration = MigrationServiceRunTimeDiagnosticConfig.Source.StartActivity("Applying anime db context migrations", ActivityKind.Client);
        await MigrateDbContext(scope.ServiceProvider.GetRequiredService<AnimeDbContext>(), cancellationToken);
        animeMigration?.Stop();

        logger.LogInformation("Migrating manga database ...");
        using var mangaMigration = MigrationServiceRunTimeDiagnosticConfig.Source.StartActivity("Applying manga db context migrations", ActivityKind.Client);
        await MigrateDbContext(scope.ServiceProvider.GetRequiredService<MangaDbContext>(), cancellationToken);
        mangaMigration?.Stop();

        hostApplicationLifetime.StopApplication();
    }

    private async Task MigrateDbContext(DbContext context, CancellationToken cancellationToken)
    {
        try
        {
            await context.Database.EnsureCreatedAsync(cancellationToken);
            await context.Database.MigrateAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            Activity.Current?.AddExceptionAndFail(ex);
            throw;
        }
    }
}