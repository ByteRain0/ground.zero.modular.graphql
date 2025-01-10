using System.Diagnostics;

namespace Rating.Api;

public static class RatingApiRunTimeDiagnosticConfig
{
    public const string ServiceName = "rating-api";

    public static string ServiceVersion = typeof(RatingApiRunTimeDiagnosticConfig).Assembly.GetName().Version?.ToString() ?? "unknown";
    
    public static ActivitySource Source = new(ServiceName);
}