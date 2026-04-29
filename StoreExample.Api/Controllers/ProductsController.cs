using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreExample.Api.Controllers.Abstractions;
using StoreExample.Application.Abstractions;
using StoreExample.Application.Models;
using StoreExample.Data;
using StoreExample.Data.Models;

namespace StoreExample.Api.Controllers;

[ApiVersion("1")]
public class ProductsController(StoreExampleDataContext context, IProductService service) : StoreExampleApiControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetProduct(CancellationToken cancellationToken)
    {
        var result = await service.Get(cancellationToken);
        return result switch
        {
            { IsSuccess: true } => Ok(result.Value),
            { IsFail: true, Error.Code: ApplicationErrors.UNEXPECTED } => InternalServerError(result),
            { IsFail: true, Error.Code: ApplicationErrors.CANCELED } => BadRequest(result),
            _ => BadRequest(result)
        };
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetProduct(int id, CancellationToken cancellationToken)
    {
        var result = await service.Get(id, cancellationToken);

        return result switch
        {
            { IsSuccess: true } => Ok(result.Value),
            { IsFail: true, Error.Code: ApplicationErrors.NOT_FOUND } => NotFound(result),
            { IsFail: true, Error.Code: ApplicationErrors.UNEXPECTED } => InternalServerError(result),
            { IsFail: true, Error.Code: ApplicationErrors.CANCELED } => BadRequest(result),
            _ => BadRequest(result)
        };
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutProduct(int id, Product product)
    {
        if (id != product.Id)
        {
            return BadRequest();
        }

        context.Entry(product).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProductExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/Products
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Product>> PostProduct(Product product)
    {
        context.Product.Add(product);
        await context.SaveChangesAsync();

        return CreatedAtAction("GetProduct", new { id = product.Id }, product);
    }

    // DELETE: api/Products/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await context.Product.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        context.Product.Remove(product);
        await context.SaveChangesAsync();

        return NoContent();
    }

    private bool ProductExists(int id)
    {
        return context.Product.Any(e => e.Id == id);
    }
}