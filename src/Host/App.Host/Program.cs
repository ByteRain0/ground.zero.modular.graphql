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

var migrationService = builder.AddProject<Projects.Japanese_Api_MigrationService>("migration-service")
    .WithReference(defaultDb);

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

var japaneseApi = builder
    .AddProject<Projects.Japanese_Api>("manga-api")
    .WithReference(defaultDb).WaitFor(defaultDb)
    .WithReference(keycloack).WaitFor(keycloack)
    .WithReference(migrationService).WaitFor(migrationService);

var ratingApi = builder
    .AddProject<Projects.Rating_Api>("rating-api")
    .WithReference(cache).WaitFor(cache);

builder.AddFusionGateway<Projects.Gateway>("fusion-gateway")
    .WithSubgraph(japaneseApi)
    .WithSubgraph(ratingApi);

builder
    .Build()
    .Compose()
    .Run();