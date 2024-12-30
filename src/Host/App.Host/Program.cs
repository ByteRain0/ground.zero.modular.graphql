using Projects;

var builder = DistributedApplication.CreateBuilder(args);

builder
    .AddOpenTelemetryCollector("collector", "otel-config.yaml")
    .WithAppForwarding()
    .WithLifetime(ContainerLifetime.Persistent);

var postgres = builder
    .AddPostgres("postgres")
    //.WithImage("ankane/pgvector")
    .WithPgAdmin()
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent);

var defaultDb = postgres.AddDatabase("default-db");

builder
    .AddProject<Japanese_Api>("dotnet-api")
    .WithReference(defaultDb).WaitFor(defaultDb);

builder.Build().Run();