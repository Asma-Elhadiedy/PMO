
namespace PMO.Application.Features.Tasks.Commands.DeleteTask;

public sealed record DeleteTaskCommand(Guid Id, string DeletedById) : IRequest<Result<bool>>;
