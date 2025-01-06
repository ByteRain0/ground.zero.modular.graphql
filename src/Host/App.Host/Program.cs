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

var cache = builder.AddRedis("cache", port: 6379)
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent)
    .WithRedisInsight();

builder
    .AddProject<Japanese_Api>("manga-api")
    .WithReference(defaultDb).WaitFor(defaultDb)
    .WithReference(keycloack).WaitFor(keycloack);

builder
    .AddProject<Rating_Api>("rating-api")
    .WithReference(cache).WaitFor(cache);

builder
    .Build()
    .Run();