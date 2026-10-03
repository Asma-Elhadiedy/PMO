
namespace PMO.Application.Features.Tasks.Commands.CreateTask;

public sealed record CreateTaskCommand(CreateTaskRequest Request, Guid ProjectId, string CreatedBy) : IRequest<Result<Guid>>;
