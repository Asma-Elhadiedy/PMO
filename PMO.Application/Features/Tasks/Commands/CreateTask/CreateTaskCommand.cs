
namespace PMO.Application.Features.Tasks.Commands.CreateTask;

public sealed record CreateTaskCommand(CreateTaskRequest Request) : IRequest<Guid>;
