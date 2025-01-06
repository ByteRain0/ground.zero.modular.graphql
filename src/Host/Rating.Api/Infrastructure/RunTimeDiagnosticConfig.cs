using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace Rating.Api.Infrastructure;

public static class RunTimeDiagnosticConfig
{
    public const string ServiceName = "rating-api";

    public static string ServiceVersion = typeof(RunTimeDiagnosticConfig).Assembly.GetName().Version?.ToString() ?? "unknown";
    
    public static ActivitySource Source = new(ServiceName);
    
    public static Meter Meter = new(ServiceName, ServiceVersion);
}