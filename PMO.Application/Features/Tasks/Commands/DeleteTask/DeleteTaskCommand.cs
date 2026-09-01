
namespace PMO.Application.Features.Tasks.Commands.DeleteTask;

internal sealed record DeleteTaskCommand(Guid Id) : IRequest<bool>;
