using Microsoft.AspNetCore.Mvc;
using MyResult;
using System.Net;

namespace StoreExample.Api.Controllers.Abstractions;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class StoreExampleApiControllerBase : ControllerBase
{
    public OkObjectResult Ok<TValue>(Result<TValue> result)
    {
        return Ok(result.Value);
    }

    private ObjectResult Problem<TValue>(HttpStatusCode statusCode, Result<TValue> result)
    {
        return base.Problem(
            detail: string.Join(", ", result.Error.Messages),
            statusCode: (int)statusCode,
            title: result.Error.Code);
    }

    public NotFoundObjectResult NotFound<TValue>(Result<TValue> result)
    {
        var problem = Problem(HttpStatusCode.NotFound, result);
        var notFound = new NotFoundObjectResult(null)
        {
            Value = problem.Value,
            ContentTypes = problem.ContentTypes,
            DeclaredType = problem.DeclaredType,
            StatusCode = problem.StatusCode,
        };

        return notFound;
    }

    public BadRequestObjectResult BadRequest<TValue>(Result<TValue> result)
    {
        var problem = Problem(HttpStatusCode.BadRequest, result);
        var badRequest = new BadRequestObjectResult(result.Value)
        {
            Value = problem.Value,
            ContentTypes = problem.ContentTypes,
            DeclaredType = problem.DeclaredType,
            StatusCode = problem.StatusCode,
        };

        return badRequest;
    }

    public ObjectResult InternalServerError<TValue>(Result<TValue> result)
    {
        var problem = Problem(HttpStatusCode.InternalServerError, result);
        var internalServerError = new ObjectResult(result.Value)
        {
            Value = problem.Value,
            ContentTypes = problem.ContentTypes,
            DeclaredType = problem.DeclaredType,
            StatusCode = problem.StatusCode,
        };

        return internalServerError;
    }
}