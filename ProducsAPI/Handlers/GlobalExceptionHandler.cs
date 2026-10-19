
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProductsAPI.Exceptions;

namespace ProductsAPI.Handlers;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(
        ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogError(
            exception,
            "An error occurred while processing the request.");

        var (statusCode, title) = exception switch
        {
            NotFoundException =>
                (StatusCodes.Status404NotFound, "Not Found"),

            ConflictException =>
                (StatusCodes.Status409Conflict, "Conflict"),

            _ =>
                (StatusCodes.Status500InternalServerError,
                 "An unexpected error occurred")
        };

        var problem = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Instance = httpContext.Request.Path
        };

        httpContext.Response.StatusCode = statusCode;

        await httpContext.Response.WriteAsJsonAsync(
            problem,
            cancellationToken);

        return true;
    }
}
