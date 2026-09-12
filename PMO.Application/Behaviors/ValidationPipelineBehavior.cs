

namespace PMO.Application.Behaviors;

public class ValidationPipelineBehavior<TRequest, TResult>(IEnumerable<IValidator<TRequest>> _validators) 
    : IPipelineBehavior<TRequest, TResult> where TRequest : notnull
{

    public async Task<TResult> Handle(TRequest request, RequestHandlerDelegate<TResult> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
            return await next(cancellationToken);

        var context = new ValidationContext<TRequest>(request);

        var results = await Task.WhenAll(
            _validators.Select(v => v.ValidateAsync(context, cancellationToken)));
        
        var failures = results
                .SelectMany(r => r.Errors)
                .Where(f => f is not null)
                .ToList();


        if (failures.Any())
        {
            var errorDictionary = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(g => g.Key, g => g.ToArray());

            throw new Domain.Exceptions.ValidationException(errorDictionary);        
        }

        return await next(cancellationToken);
    }
}
