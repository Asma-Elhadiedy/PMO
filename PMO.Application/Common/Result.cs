
namespace PMO.Application.Common;

public class Result<T>
{
    public bool IsSuccess { get; }
    public T? Data { get; }
    public string? Error { get; }
    public T? Errors { get; }

    private Result(bool isSuccess, T? data, string? error, T? errors)
    {
        IsSuccess = isSuccess;
        Data = data;
        Error = error;
        Errors = errors;
    }

    public static Result<T> Success(T data) => new(true, data, null, default);
    public static Result<T> Failure(string error) => new(false, default, error, default);
    public static Result<T> Failures(T errors) => new(false, default, null, errors);

}