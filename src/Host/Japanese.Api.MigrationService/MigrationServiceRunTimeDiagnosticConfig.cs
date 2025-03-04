using System.Diagnostics;

namespace Japanese.Api.MigrationService;

public static class MigrationServiceRunTimeDiagnosticConfig
{
    public const string ServiceName = "migration-service";

    public static string ServiceVersion = typeof(MigrationServiceRunTimeDiagnosticConfig).Assembly.GetName().Version?.ToString() ?? "unknown";

    public static ActivitySource Source = new(ServiceName);
}