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

var database = postgres.AddDatabase("default-db");

builder
    .AddProject<Japanese_Api>("dotnet-api")
    .WithReference(database)
    .WaitFor(postgres)
    .WaitFor(database);

builder.Build().Run();