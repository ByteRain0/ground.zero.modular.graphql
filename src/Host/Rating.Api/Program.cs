using Rating.Api.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddRedisClient(connectionName: "cache");

builder.Services
    .AddSingleton<DatabaseMock>()
    .AddMediatR(config => { config.RegisterServicesFromAssembly(typeof(Program).Assembly); });

builder
    .Services
    .AddGraphQLServer()
    .ModifyOptions(options =>
    {
        options.DefaultBindingBehavior = BindingBehavior.Explicit;
        options.EnsureAllNodesCanBeResolved = true;
    })
    .AddRatingTypes()
    .AddQueryConventions()
    .AddInstrumentation();

var app = builder.Build();


app.MapDefaultEndpoints();

app.MapGraphQL();

app.RunWithGraphQLCommands(args);