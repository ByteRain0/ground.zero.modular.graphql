using Core.Background;
using Core.Environment;
using Hangfire;
using Japanese.Api.Infrastructure;
using Japanese.Api.Migrations;

var builder = WebApplication
    .CreateBuilder(args)
    .AddDynamicEntitiesConfigurations();

builder
    .AddServiceDefaults()
    .AddApplicationServices()
        .Services
            .AddBackgroundJobs(builder.Configuration)
            .AddKeyCloackBasedAuth()
            .AddGraphQLInfrastructure();


var app = builder.Build();

// if (AppHost.IsDevelopment())
// {
//     app.ApplyAnimeMigrations();
//     app.ApplyMangaMigrations();
// }

app.MapDefaultEndpoints();

app.MapGraphQL();
// app.MapHangfireDashboard(options: new DashboardOptions()
// {
//     StatsPollingInterval = 60000
// });

app.RunWithGraphQLCommands(args);