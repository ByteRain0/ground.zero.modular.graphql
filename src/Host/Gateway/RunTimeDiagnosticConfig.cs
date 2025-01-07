using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace Gateway;

public static class RunTimeDiagnosticConfig
{
    public const string ServiceName = "fusion-gateway";

    public static string ServiceVersion = typeof(RunTimeDiagnosticConfig).Assembly.GetName().Version?.ToString() ?? "unknown";
    
    public static ActivitySource Source = new(ServiceName);
    
    public static Meter Meter = new(ServiceName, ServiceVersion);
}