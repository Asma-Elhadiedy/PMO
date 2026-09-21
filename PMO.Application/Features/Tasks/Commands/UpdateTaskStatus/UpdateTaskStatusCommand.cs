
namespace PMO.Application.Features.Tasks.Commands.UpdateTaskStatus;

public sealed record UpdateTaskStatusCommand(Guid TaskId) : IRequest<Result<bool>>;
