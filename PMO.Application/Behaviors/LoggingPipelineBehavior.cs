

using System.Diagnostics;

namespace PMO.Application.Behaviors;

public class LoggingPipelineBehavior<TRequest, TResult>(ILogger<LoggingPipelineBehavior<TRequest, TResult>> _logger) 
    : IPipelineBehavior<TRequest, TResult> where TRequest : notnull
{
    public async Task<TResult> Handle(TRequest request, RequestHandlerDelegate<TResult> next, CancellationToken cancellationToken)
    {
        Stopwatch sw = Stopwatch.StartNew();
        _logger.LogInformation("Handling {RequestName} with content: {@Request}", typeof(TRequest).Name, request);
        var response = await next(cancellationToken);
        _logger.LogInformation("Handled {RequestName} with response: {@Response}", typeof(TRequest).Name, response);
        sw.Stop();
        _logger.LogInformation("Handled {RequestName} in {ElapsedMilliseconds} ms", typeof(TRequest).Name, sw.ElapsedMilliseconds);
        return response;
    }
}