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

var keycloack = builder
    .AddKeycloak(name: "keycloack", port: 8080)
    .WithDataVolume()
    .WithRealmImport("../../../configurations/keycloack")
    .WithExternalHttpEndpoints()
    .WithLifetime(ContainerLifetime.Persistent);

builder
    .AddProject<Japanese_Api>("dotnet-api")
    .WithReference(defaultDb).WaitFor(defaultDb)
    .WithReference(keycloack).WaitFor(keycloack);

builder
    .Build()
    .Run();