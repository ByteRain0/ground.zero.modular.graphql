using Core.Background;
using Core.Environment;
using Core.Otel;
using Hangfire;
using Japanese.Api.Infrastructure;
using Japanese.Api.Migrations;

var builder = WebApplication
    .CreateBuilder(args)
    .AddTelemetry();

builder.Services
    .AddApplicationServices(builder.Configuration)
    .AddBackgroundJobs(builder.Configuration) // Set only in the Program.cs to make testing easier
    .AddGraphQLInfrastructure();

var app = builder.Build();

if (AppHost.IsDevelopment())
{
    app.ApplyAnimeMigrations();
    app.ApplyMangaMigrations();
}

app.MapGraphQL();
app.MapHangfireDashboard(options: new DashboardOptions()
{
    StatsPollingInterval = 60000
});

app.RunWithGraphQLCommands(args);