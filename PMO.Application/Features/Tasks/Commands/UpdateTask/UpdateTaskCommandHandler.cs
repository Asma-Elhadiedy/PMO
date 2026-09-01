
namespace PMO.Application.Features.Tasks.Commands.UpdateTask;

internal sealed class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, bool>
{
    public Task<bool> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
