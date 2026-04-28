using Microsoft.EntityFrameworkCore;
using StoreExample.Data;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

var connectionStringDatabase = configuration.GetConnectionString("StoreExampleApiContext");

ArgumentException.ThrowIfNullOrEmpty(connectionStringDatabase);

builder.Services.AddDbContext<StoreExampleDataContext>(options =>
    options.UseSqlite(connectionStringDatabase));

var services = builder.Services;

services.AddControllers();
services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();