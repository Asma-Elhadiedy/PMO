
namespace PMO.Application.Features.Tasks.Commands.UpdateTask;

public sealed record UpdateTaskCommand(UpdateTaskRequest Request, Guid TaskId, Guid ProjectId, string UpdatedBy) : IRequest<Result<bool>>;
