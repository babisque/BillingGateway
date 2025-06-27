using System.Diagnostics.CodeAnalysis;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using BillingGateway.Application.Handlers.Customer;
using BillingGateway.Application.Profiles;
using BillingGateway.Application.Settings;
using BillingGateway.Domain.Entities;
using BillingGateway.Domain.Interfaces;
using BillingGateway.Infrastructure;
using BillingGateway.Infrastructure.Repositories;

namespace BillingGateway.API.Extensions;

[ExcludeFromCodeCoverage]
public static class ServiceCollectionExtensions
{
    public static void AddPaymentCore(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddDatabase(configuration)
            .AddIdentity()
            .AddCustomAuthentication(configuration)
            .AddRepositories()
            .AddSwagger()
            .AddApplicationServices();
    }
    
    private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(opts =>
            opts.UseSqlServer(configuration.GetConnectionString("PaymentSqlServer")));
        
        return services;
    }
    
    private static IServiceCollection AddIdentity(this IServiceCollection services)
    {
        services.AddIdentity<Customer, IdentityRole>(opts => {
                // identity configurations is here
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
            
        return services;
    }
    
    private static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection("Jwt"));
        var jwt = configuration.GetSection("Jwt").Get<JwtSettings>()
                  ?? throw new InvalidOperationException("JWT is not configured properly.");
        services.AddSingleton(jwt);

        var keyBytes = Encoding.UTF8.GetBytes(jwt.Key);

        services.AddAuthentication(opts =>
            {
                opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(opts =>
            {
                opts.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwt.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwt.Audience,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
                    ValidateIssuerSigningKey = true
                };
            });
            
        return services;
    }
    
    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        
        return services;
    }
    
    private static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer(); 
        
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "SuaGyn API", Version = "v1" });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "Insert your JWT Bearer token here. Example: `Bearer {your_token}`",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                BearerFormat = "JWT"
            });

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
    
    private static IServiceCollection AddCustomCors(this IServiceCollection services) => services;
    private static IServiceCollection ConfigureTokenLifespan(this IServiceCollection services, IConfiguration configuration) => services;
    private static IServiceCollection AddSingletons(this IServiceCollection services, IConfiguration configuration) => services;
    private static IServiceCollection AddDocumentSignEventStrategies(this IServiceCollection services) => services;
    private static IServiceCollection AddProcessInstanceMergeStrategies(this IServiceCollection services) => services;
    public static IServiceCollection AddStartupTasks(this IServiceCollection services) => services;
}