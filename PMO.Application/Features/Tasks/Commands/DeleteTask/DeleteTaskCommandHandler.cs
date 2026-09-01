
namespace PMO.Application.Features.Tasks.Commands.DeleteTask;

internal sealed class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, bool>
{
    public Task<bool> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
