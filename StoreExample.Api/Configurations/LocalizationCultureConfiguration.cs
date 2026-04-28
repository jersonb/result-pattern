namespace StoreExample.Api.Configurations;

internal static class LocalizationCultureConfiguration
{
    internal static IServiceCollection AddLocalizationResources(this IServiceCollection services)
    {
        services.AddLocalization(options => options.ResourcesPath = "Resources");

        return services;
    }

    internal static IApplicationBuilder UseCustomRequestLocalization(this IApplicationBuilder app)
    {
        var supportedCultures = new[] { "en", "pt-BR" };
        var localizationOptions = new RequestLocalizationOptions()
            .SetDefaultCulture(supportedCultures[0])
            .AddSupportedCultures(supportedCultures);

        localizationOptions.ApplyCurrentCultureToResponseHeaders = true;
        app.UseRequestLocalization(localizationOptions);
        return app;
    }
}