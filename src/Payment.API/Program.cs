using Payment.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPaymentCore(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddAuthorization();

var app = builder.Build();

app.UsePaymentPipeline();

app.Run();