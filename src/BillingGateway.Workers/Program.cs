using BillingGateway.Workers.Extensions;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddApplicationDbContext(builder.Configuration);
builder.Services.AddCustomMassTransit(builder.Configuration);

var host = builder.Build();
host.Run();