using System.Reflection;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Messaging;

public static class MessagingServiceCollectionExtensions
{
    public static IServiceCollection AddRabbitMqWithMasstransit(
        this IServiceCollection services,
        IConfiguration configuration,
        Assembly[] assembliesWithConsumers)
    {
        // TODO: in the future consider adding outbox to Masstransit
        services.AddMassTransit(x =>
        {
            x.AddConsumers(assembliesWithConsumers);
            x.SetKebabCaseEndpointNameFormatter();

            x.UsingRabbitMq((context, cfg) =>
            {
                var rabbitMqConnectionString = configuration.GetConnectionString("rabbitmq");
                if (rabbitMqConnectionString is not null)
                {
                    cfg.Host(configuration.GetConnectionString("rabbitmq")!);

                }
                cfg.ConfigureEndpoints(context);
            });


        });

        services.AddScoped<IMessageSender, MessageSender>();

        return services;
    }
}