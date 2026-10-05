using Microsoft.AspNetCore.Diagnostics;

namespace PMO.API.Middleware;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> _logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is Domain.Exceptions.ValidationException validationException)

        {
            _logger.LogWarning("Validation failed: {Message}", validationException.Message);

            var problemDetails = Result<IDictionary<string, string[]>>.Failures(validationException.Errors);

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
            return true;
        }

        if (exception is Domain.Exceptions.InvalidTaskStatusTransitionException invalidTaskStatusTransitionException)
        {
            _logger.LogWarning("Invalid task status transition: {Message}", invalidTaskStatusTransitionException.Message);

            var problemDetails = Result<string>.Failure(invalidTaskStatusTransitionException.Message);

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
            return true;
        }

        return false;
    }
}
