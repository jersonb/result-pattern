using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using StoreExample.Api.Configurations;
using StoreExample.Application;
using StoreExample.Application.Abstractions;
using StoreExample.Data;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

var connectionStringDatabase = configuration.GetConnectionString("StoreExampleApiContext");

ArgumentException.ThrowIfNullOrEmpty(connectionStringDatabase);

services.AddDbContext<StoreExampleDataContext>(options =>
    options.UseSqlite(connectionStringDatabase));

services.AddScoped<IProductService, ProductService>();

services.AddControllers();

services.AddCustomOpenApi();
services.AddLocalizationResources();

var app = builder.Build();

app.UseCustomCors();
app.UseCustomRequestLocalization();

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