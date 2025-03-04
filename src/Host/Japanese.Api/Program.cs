using Core.Aspire;
using Core.Background;
using Core.Messaging;
using Japanese.Api.Infrastructure;

var builder = WebApplication
    .CreateBuilder(args)
    .AddDynamicEntitiesConfigurations();

builder
    .AddServiceDefaults()
    .AddApplicationServices()
        .Services
            .AddBackgroundJobs(builder.Configuration)
            .AddKeyCloakBasedAuth()
            .AddRabbitMqWithMasstransit(
                configuration: builder.Configuration,
                assembliesWithConsumers:
                [
                    typeof(ApplicationBuilderExtensions).Assembly
                ])
            .AddGraphQLInfrastructure();

var app = builder.Build();

app.MapDefaultEndpoints();

app.MapGraphQL();
// app.MapHangfireDashboard(options: new DashboardOptions()
// {
//     StatsPollingInterval = 60000
// });

app.RunWithGraphQLCommands(args);