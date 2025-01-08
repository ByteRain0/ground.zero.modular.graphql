using Core.Aspire;
using Core.Background;
using Japanese.Api.Infrastructure;

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

app.MapDefaultEndpoints();

app.MapGraphQL();
// app.MapHangfireDashboard(options: new DashboardOptions()
// {
//     StatsPollingInterval = 60000
// });

app.RunWithGraphQLCommands(args);