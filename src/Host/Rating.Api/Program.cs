using Core.Aspire;
using Core.Messaging;
using Rating.Api;
using Rating.Api.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => 
        tracing.AddSource(RatingApiRunTimeDiagnosticConfig.Source.Name));

builder.AddRedisClient(connectionName: "cache");

builder.Services.AddRabbitMqWithMasstransit(
    configuration: builder.Configuration,
    assembliesWithConsumers:
    [
        typeof(Program).Assembly
    ]);

builder.Services
    .AddSingleton<DatabaseMock>()
    .AddMediatR(config => { config.RegisterServicesFromAssembly(typeof(Program).Assembly); });

builder
    .Services
    .AddGraphQLServer()
    .ModifyOptions(options =>
    {
        options.DefaultBindingBehavior = BindingBehavior.Explicit;
        options.EnsureAllNodesCanBeResolved = false;
    })
    .AddRatingTypes()
    .AddQueryConventions()
    .AddInstrumentation()
    .AddGlobalObjectIdentification();

var app = builder.Build();


app.MapDefaultEndpoints();

app.MapGraphQL();

app.RunWithGraphQLCommands(args);