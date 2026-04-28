namespace StoreExample.Api.Configurations;

internal static class CorsConfiguration
{
    internal static IApplicationBuilder UseCustomCors(this IApplicationBuilder app)
    {
        app.UseCors(policy =>

            policy
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin()
        );
        return app;
    }
}