using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace Core.Otel;

public static class RunTimeDiagnosticConfig
{
    public const string ServiceName = "dotnet-api";

    public static string ServiceVersion = typeof(OpenTelemetryWebApplicationBuilderExtensions).Assembly.GetName().Version?.ToString() ?? "unknown";
    
    public static ActivitySource Source = new(ServiceName);
    
    public static Meter Meter = new(ServiceName, ServiceVersion);
}