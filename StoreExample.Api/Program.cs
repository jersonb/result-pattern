using Asp.Versioning;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using StoreExample.Data;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

var connectionStringDatabase = configuration.GetConnectionString("StoreExampleApiContext");

ArgumentException.ThrowIfNullOrEmpty(connectionStringDatabase);

services.AddDbContext<StoreExampleDataContext>(options =>
    options.UseSqlite(connectionStringDatabase));

services.AddControllers();

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

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateAsyncScope();
var context = scope.ServiceProvider.GetRequiredService<StoreExampleDataContext>();
await context.Database.MigrateAsync();

await app.RunAsync();