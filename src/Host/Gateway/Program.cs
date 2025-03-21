using Core.Aspire;
using Gateway;
using Gateway.Infrastructure;
using Microsoft.Extensions.Http;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing.AddSource(GatewayRunTimeDiagnosticConfig.Source.Name));

builder.Services
    .AddHttpContextAccessor()
    .AddScoped<AuthenticationMiddleware>()
    .ConfigureAll<HttpClientFactoryOptions>(opt =>
    {
        opt.HttpMessageHandlerBuilderActions.Add(builder =>
        {
            builder.AdditionalHandlers.Add(builder.Services.GetRequiredService<AuthenticationMiddleware>());
        });
    });

builder.Services
    .AddCors()
    .AddHeaderPropagation(c =>
    {
        c.Headers.Add("GraphQL-Preflight");
        c.Headers.Add("Authorization");
    });

builder.Services
    .AddHttpClient("Fusion")
    .AddHeaderPropagation();

builder.Services
    .AddFusionGatewayServer()
    .ConfigureFromFile("./gateway.fgp")
    .AddServiceDiscoveryRewriter()
    .CoreBuilder.ModifyCostOptions(opts =>
    {
        opts.MaxFieldCost = 3_000;
        opts.MaxTypeCost = 3_000;
    });


var app = builder.Build();

app.MapDefaultEndpoints();
app.UseWebSockets();
app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseHeaderPropagation();
app.MapGraphQL();

app.RunWithGraphQLCommands(args);