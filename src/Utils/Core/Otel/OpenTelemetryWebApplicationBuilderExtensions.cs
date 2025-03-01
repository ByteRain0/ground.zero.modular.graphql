using MassTransit.Logging;
using MassTransit.Monitoring;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;
using OpenTelemetry.Exporter;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Core.Otel;

public static class OpenTelemetryWebApplicationBuilderExtensions
{
    public static IHostApplicationBuilder AddTelemetry(this IHostApplicationBuilder builder)
    {
        OpenTelemetrySettings telemetrySettings = builder
            .Configuration
            .GetSection(nameof(OpenTelemetrySettings))
            .Get<OpenTelemetrySettings>()!;

        if (!telemetrySettings.Enabled)
        {
            // In case the app is running in Test mode
            return builder;
        }

        builder.Logging.AddOpenTelemetry(config =>
        {
            var resourceBuilder = ResourceBuilder.CreateDefault();
            config.SetResourceBuilder(resourceBuilder);
            config.IncludeScopes = true;
            config.IncludeFormattedMessage = true;
            config.ParseStateValues = true;
            config.AddOtlpExporter(exporterOptions => { exporterOptions.Endpoint = telemetrySettings.TracesEndpoint; });
        });

        builder.Services
            .AddOpenTelemetry()
            .WithTracing(traceProviderBuilder =>
                traceProviderBuilder
                    .AddNpgsql()
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddHotChocolateInstrumentation()
                    .AddEntityFrameworkCoreInstrumentation()
                    .AddSource(DiagnosticHeaders.DefaultListenerName) // Masstransit ActivitySource
                    .AddOtlpExporter(options =>
                    {
                        options.Endpoint = telemetrySettings.TracesEndpoint;
                        options.Protocol = OtlpExportProtocol.Grpc;
                    })
            )
            .WithMetrics(meterProviderBuilder =>
                meterProviderBuilder
                    .AddHttpClientInstrumentation()
                    .AddAspNetCoreInstrumentation()
                    .AddOtlpExporter(options =>
                    {
                        options.Endpoint = telemetrySettings.TracesEndpoint;
                        options.Protocol = OtlpExportProtocol.Grpc;
                    })
                    .AddMeter(
                        "System.Runtime",
                        "Microsoft.AspNetCore.Hosting",
                        "Microsoft.AspNetCore.Server.Kestrel",
                        InstrumentationOptions.MeterName // Masstransit metrics
                    ));

        return builder;
    }
}