

using System.Diagnostics;

namespace PMO.Application.Behaviours;

public class LoggingPipelineBehaviour<TRequest, TResult>(ILogger<LoggingPipelineBehaviour<TRequest, TResult>> _logger) : IPipelineBehavior<TRequest, TResult>
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