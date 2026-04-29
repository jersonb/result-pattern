using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using MyResult;
using StoreExample.Application.Abstractions;
using StoreExample.Application.Models;
using StoreExample.Data;

namespace StoreExample.Application;

public class ProductService(
    ILogger<ProductService> logger,
    IStringLocalizer<ProductService> localizer,
    StoreExampleDataContext context): IProductService
{
    public async Task<Result<List<ProductDto>>> Get(CancellationToken cancellationToken)
    {
        try
        {
            var products = await context
               .Product.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Sales.Count))
               .ToListAsync(cancellationToken);

            return products;
        }
        catch (OperationCanceledException ex)
        {
            logger.LogWarning(ex, "Operation was cancelled while retrieving products");

            return ApplicationErrors.Canceled
                .AddMessage(localizer[ApplicationErrors.CANCELED])
                .AddException(ex);
        }
        catch (InvalidOperationException ex)
        {
            logger.LogError(ex, "Malformation LINQ query, problem to execute query.");
            return ApplicationErrors.EfLinkQuery
                .AddMessage(localizer[ApplicationErrors.EF_LINQ_QUERY]);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while retrieving products");
            return ApplicationErrors.Unexpected
                .AddMessage(localizer[ApplicationErrors.UNEXPECTED])
                .AddException(ex);
        }
    }

    public async Task<Result<ProductDto>> Get(int id, CancellationToken cancellationToken)
    {
        try
        {
            var product = await context.Product
                .Where(p => p.Id == id)
                .Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Sales.Count))
                .FirstOrDefaultAsync(cancellationToken);

            if (product is null)
            {
                return ApplicationErrors.NotFound
                    .AddMessage(localizer[ApplicationErrors.NOT_FOUND]);
            }

            return product;
        }
        catch (OperationCanceledException ex)
        {
            logger.LogWarning(ex, "Operation was cancelled while retrieving product {Id}", id);

            return ApplicationErrors.Canceled
                .AddMessage(localizer[ApplicationErrors.CANCELED])
                .AddException(ex);
        }
        catch (InvalidOperationException ex)
        {
            logger.LogError(ex, "Malformation LINQ query, problem to execute query.");
            return ApplicationErrors.EfLinkQuery
                .AddMessage(localizer[ApplicationErrors.EF_LINQ_QUERY]);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while retrieving product with id {Id}", id);
            return ApplicationErrors.Unexpected
                .AddMessage(localizer[ApplicationErrors.UNEXPECTED])
                .AddException(ex);
        }
    }
}