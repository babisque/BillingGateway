using Microsoft.AspNetCore.HttpOverrides;

namespace Payment.API.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void UsePaymentPipeline(this WebApplication app)
    {
        var forwardedHeadersOptions = new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
        };

        forwardedHeadersOptions.KnownNetworks.Clear();
        forwardedHeadersOptions.KnownProxies.Clear();

        app.UseForwardedHeaders(forwardedHeadersOptions);

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SuaGyn API v1");
            });
        }
        
        app.UseCors("_myAllowSpecificOrigins");

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
    }
}