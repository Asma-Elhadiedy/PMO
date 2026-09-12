
using Microsoft.AspNetCore.Diagnostics;

namespace PMO.API.Middleware;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> _logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is not Domain.Exceptions.ValidationException validationException)        
            return false;
        

        _logger.LogWarning("Validation failed: {Message}", validationException.Message);

        var problemDetails = Result<IDictionary<string, string[]>>.Failures(validationException.Errors);


        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
        return true;
    }
}