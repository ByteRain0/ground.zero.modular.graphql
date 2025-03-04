using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace Core.Otel.Sources;

public static class JapaneseApiRunTimeDiagnosticConfig
{
    public const string ServiceName = "manga-api";

    public static string ServiceVersion = typeof(OpenTelemetryWebApplicationBuilderExtensions).Assembly.GetName().Version?.ToString() ?? "unknown";

    public static ActivitySource Source = new(ServiceName);

    public static Meter Meter = new(ServiceName, ServiceVersion);
}