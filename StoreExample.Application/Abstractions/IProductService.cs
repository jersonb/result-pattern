using MyResult;
using StoreExample.Application.Models;

namespace StoreExample.Application.Abstractions;

public interface IProductService
{
    Task<Result<List<ProductDto>>> Get(CancellationToken cancellationToken);

    Task<Result<ProductDto>> Get(int id, CancellationToken cancellationToken);
}