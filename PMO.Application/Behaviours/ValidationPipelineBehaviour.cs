

namespace PMO.Application.Behaviours;

public class ValidationPipelineBehaviour<IRequest, TResult>(IEnumerable<IValidator<IRequest>> _validator) : IPipelineBehavior<IRequest, TResult>
{

    public async Task<TResult> Handle(IRequest request, RequestHandlerDelegate<TResult> next, CancellationToken cancellationToken)
    {
        if (!_validator.Any())
            return await next(cancellationToken);

        var validationResult = _validator
            .Select(v => v.Validate(request))
            .SelectMany(r => r.Errors);

        if (validationResult.Any())
        {
            return (TResult)validationResult;
        }

        return await next(cancellationToken);
    }
}
