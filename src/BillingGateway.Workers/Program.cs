using BillingGateway.Domain.Interfaces;
using BillingGateway.Infrastructure;
using BillingGateway.Infrastructure.Repositories;
using BillingGateway.Workers;
using BillingGateway.Workers.Consumers;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        IConfiguration configuration = hostContext.Configuration;

        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IPlanRepository, PlanRepository>();
        services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();

        services.AddDbContext<ApplicationDbContext>(opts =>
            opts.UseSqlServer(configuration.GetConnectionString("PaymentSqlServer")));
            
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
    })
    .Build();
host.Run();