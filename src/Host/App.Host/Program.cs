var builder = DistributedApplication.CreateBuilder(args);

var collector = builder
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

var keycloak = builder
    .AddKeycloak(name: "keycloak", port: 8080)
    .WithDataVolume()
    .WithRealmImport("../../../configurations/keycloak")
    .WithExternalHttpEndpoints()
    .WithLifetime(ContainerLifetime.Persistent);

var japaneseApi = builder
    .AddProject<Projects.Japanese_Api>("manga-api")
    .WithReference(defaultDb).WaitFor(defaultDb)
    .WithReference(keycloak).WaitFor(keycloak)
    .WithReference(migrationService).WaitFor(migrationService);

bool.TryParse(Environment.GetEnvironmentVariable("START_WITH_RATING_API"), out var startWithRatingApi );
if (startWithRatingApi)
{
    var cache = builder.AddRedis("cache", port: 6379)
        .WithDataVolume()
        .WithLifetime(ContainerLifetime.Persistent)
        .WithRedisInsight();
    
    var ratingApi = builder
        .AddProject<Projects.Rating_Api>("rating-api")
        .WithReference(cache).WaitFor(cache);

    builder.AddFusionGateway<Projects.Gateway>("fusion-gateway")
        .WithSubgraph(japaneseApi)
        .WithSubgraph(ratingApi);
}

builder
    .Build()
    .Compose()
    .Run();