using System.Diagnostics;

namespace Gateway.Infrastructure;

public static class GatewayRunTimeDiagnosticConfig
{
    public const string ServiceName = "fusion-gateway";

    public static string ServiceVersion = typeof(GatewayRunTimeDiagnosticConfig).Assembly.GetName().Version?.ToString() ?? "unknown";

    public static ActivitySource Source = new(ServiceName);
}