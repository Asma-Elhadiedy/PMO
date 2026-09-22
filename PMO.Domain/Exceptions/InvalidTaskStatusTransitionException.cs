

namespace PMO.Domain.Exceptions;

public class InvalidTaskStatusTransitionException(ETaskStatus current, ETaskStatus requested) : Exception($"Cannot transition task from '{current}' to '{requested}'.")
{
    public ETaskStatus CurrentStatus { get; } = current;
    public ETaskStatus RequestedStatus { get; } = requested;
}