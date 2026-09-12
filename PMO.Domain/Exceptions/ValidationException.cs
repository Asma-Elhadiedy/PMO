

namespace PMO.Domain.Exceptions;

public class ValidationException : Exception
{
    public IDictionary<string, string[]> Errors { get; } = new Dictionary<string, string[]>();
   
    public ValidationException(IDictionary<string, string[]> errors) : base("One or more validation failures have occurred.")
    {
        Errors = errors;
    }
}
