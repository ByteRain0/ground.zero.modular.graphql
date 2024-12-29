using Projects;

var builder = DistributedApplication.CreateBuilder(args);

builder
    .AddOpenTelemetryCollector("collector", "otel-config.yaml")
    .WithAppForwarding()
    .WithLifetime(ContainerLifetime.Persistent);

var postgres = builder
    .AddPostgres("postgres")
    .WithPgAdmin()
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent);

var mangaDatabase = postgres.AddDatabase("manga-db");
var animeDatabase = postgres.AddDatabase("anime-db");
var jobsDatabase = postgres.AddDatabase("jobs-db");

builder
    .AddProject<Japanese_Api>("dotnet-api")
    .WithReference(mangaDatabase)
    .WithReference(animeDatabase)
    .WithReference(jobsDatabase)
    .WaitFor(postgres);

builder.Build().Run();