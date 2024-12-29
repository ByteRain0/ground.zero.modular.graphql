using Projects;

var builder = DistributedApplication.CreateBuilder(args);

// builder
//     .AddOpenTelemetryCollector("collector", "otel-config.yaml")
//     .WithAppForwarding();

builder
    .AddProject<Japanese_Api>("dotnet-api");


builder.Build().Run();