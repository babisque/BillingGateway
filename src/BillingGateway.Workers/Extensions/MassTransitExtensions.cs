using BillingGateway.Workers.Consumers;
using MassTransit;

namespace BillingGateway.Workers.Extensions
{
    public static class MassTransitExtensions
    {
        public static IServiceCollection AddCustomMassTransit(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumers(typeof(SubscriptionCreatedConsumer).Assembly);

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(configuration.GetConnectionString("RabbitMQ"));

                    cfg.ReceiveEndpoint("subscription-created-queue", e =>
                    {
                        e.ConfigureConsumer<SubscriptionCreatedConsumer>(context);
                    });

                    cfg.ConfigureEndpoints(context);
                });
            });
            return services;
        }
    }
}

