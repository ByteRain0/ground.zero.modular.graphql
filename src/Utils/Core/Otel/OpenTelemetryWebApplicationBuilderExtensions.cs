using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;
using OpenTelemetry;
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

        // Action<ResourceBuilder> configureResource = resourceBuilder =>
        // {
        //     resourceBuilder.AddService(
        //         serviceName: RunTimeDiagnosticConfig.ServiceName,
        //         serviceVersion: RunTimeDiagnosticConfig.ServiceVersion,
        //         serviceInstanceId: System.Environment.MachineName);
        //     resourceBuilder.AddAttributes(telemetrySettings.GetOtelAttributes());
        //     resourceBuilder.AddAttributes(telemetrySettings.GetOtelAttributes());
        // };

        builder.Logging.AddOpenTelemetry(config =>
        {
            var resourceBuilder = ResourceBuilder.CreateDefault();
            //configureResource(resourceBuilder);
            config.SetResourceBuilder(resourceBuilder);
            config.IncludeScopes = true;
            config.IncludeFormattedMessage = true;
            config.ParseStateValues = true;
            config.AddOtlpExporter(exporterOptions => { exporterOptions.Endpoint = telemetrySettings.TracesEndpoint; });
        });

        builder.Services
            .AddOpenTelemetry()
            //.ConfigureResource(configureResource)
            .WithTracing(traceProviderBuilder =>
                traceProviderBuilder
                    .AddNpgsql() // Uncomment in case you need to see db requests. Warning generates plethora of spans.
                    .AddSource(RunTimeDiagnosticConfig.Source.Name)
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddHotChocolateInstrumentation()
                    .AddEntityFrameworkCoreInstrumentation()
                    .AddProcessor(new BatchActivityExportProcessor(new SimpleLinkExporter()))
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
                        RunTimeDiagnosticConfig.Meter.Name
                    ));

        return builder;
    }
}