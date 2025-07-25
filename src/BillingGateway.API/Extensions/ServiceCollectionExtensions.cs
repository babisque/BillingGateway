using System.Diagnostics.CodeAnalysis;
using BillingGateway.Application.Handlers.Customer.Register;
using BillingGateway.Application.Profiles;
using BillingGateway.Domain.Interfaces;
using BillingGateway.Infrastructure;
using BillingGateway.Infrastructure.Repositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace BillingGateway.API.Extensions;

[ExcludeFromCodeCoverage]
public static class ServiceCollectionExtensions
{
    public static void AddBillingGatewayCore(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddDatabase(configuration)
            .AddRepositories()
            .AddSwagger()
            .AddApplicationServices()
            .AddMassTransitConfig(configuration);
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(opts =>
            opts.UseSqlServer(configuration.GetConnectionString("PaymentSqlServer")));

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IPlanRepository, PlanRepository>();
        services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();

        return services;
    }

    private static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "SuaGyn API", Version = "v1" });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        return services;
    }

    private static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(CustomerMapper).Assembly);

        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(RegisterCustomerHandler).Assembly));

        return services;
    }

    private static IServiceCollection AddMassTransitConfig(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) => { cfg.Host(configuration.GetConnectionString("RabbitMQ")); });
        });

        return services;
    }

    private static IServiceCollection AddCustomAuthentication(this IServiceCollection services,
        IConfiguration configuration) => services;

    private static IServiceCollection AddIdentity(this IServiceCollection services) => services;
    private static IServiceCollection AddCustomCors(this IServiceCollection services) => services;

    private static IServiceCollection ConfigureTokenLifespan(this IServiceCollection services,
        IConfiguration configuration) => services;

    private static IServiceCollection AddSingletons(this IServiceCollection services, IConfiguration configuration) =>
        services;

    private static IServiceCollection AddDocumentSignEventStrategies(this IServiceCollection services) => services;
    private static IServiceCollection AddProcessInstanceMergeStrategies(this IServiceCollection services) => services;
    public static IServiceCollection AddStartupTasks(this IServiceCollection services) => services;
}