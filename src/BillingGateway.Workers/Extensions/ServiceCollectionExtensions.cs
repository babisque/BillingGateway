using BillingGateway.Application.Interfaces.Services;
using BillingGateway.Application.Services;
using BillingGateway.Application.Settings;
using BillingGateway.Domain.Interfaces;
using BillingGateway.Infrastructure;
using BillingGateway.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BillingGateway.Workers.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SmtpSettings>(configuration.GetSection("SmtpSettings"));
            services.AddTransient<IEmailService, EmailService>();
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IPlanRepository, PlanRepository>();
            services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
            return services;
        }

        public static IServiceCollection AddApplicationDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(opts =>
                opts.UseSqlServer(configuration.GetConnectionString("PaymentSqlServer")));
            return services;
        }
    }
}

