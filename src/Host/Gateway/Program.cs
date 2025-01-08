using Core.Aspire;
using Gateway;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => 
        tracing.AddSource(GatewayRunTimeDiagnosticConfig.Source.Name));

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
    .AddServiceDiscoveryRewriter();

var app = builder.Build();

app.MapDefaultEndpoints();
app.UseWebSockets();
app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseHeaderPropagation();
app.MapGraphQL();

app.RunWithGraphQLCommands(args);