
namespace PMO.Application.Features.Tasks.Commands.UpdateTask;

public sealed record UpdateTaskCommand(UpdateTaskRequest Request) : IRequest<bool>;
