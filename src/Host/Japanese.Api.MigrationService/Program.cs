using Anime.Service.Infrastructure.Data;
using Core.Aspire;
using Japanese.Api.MigrationService;
using Japanese.Api.MigrationService.Workers;
using Manga.Service.Infrastructure.Data;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddOpenTelemetry()
    .WithTracing(tracing =>
        tracing.AddSource(MigrationServiceRunTimeDiagnosticConfig.Source.Name));

builder.AddNpgsqlDbContext<AnimeDbContext>("default-db", c => c.DisableTracing = true);
builder.AddNpgsqlDbContext<MangaDbContext>("default-db", c => c.DisableTracing = true);

builder.Services.AddHostedService<DbMigrator>();

var host = builder.Build();
host.Run();
