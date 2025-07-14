using BillingGateway.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddBillingGatewayCore(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddAuthorization();

var app = builder.Build();

app.UsePaymentPipeline();

app.Run();