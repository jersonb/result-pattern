using Asp.Versioning;

namespace StoreExample.Api.Configurations;

public static class OpenApiConfiguration
{
    internal static IServiceCollection AddCustomOpenApi(this IServiceCollection services) 
    {
        services.AddOpenApi(options =>
        {
            options.AddDocumentTransformer((document, context, cancelationToken) =>
            {
                document.Info.Title = "Store Example API";
                document.Info.Description = "An example API for a store.";
                document.Info.Version = "v1";
                return Task.CompletedTask;
            });
        });

        services
            .AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
            }).AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

        return services;
    }
}

