
namespace PMO.Application.Features.Tasks.Commands.CreateTask;

internal sealed class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, bool>
{
    public Task<bool> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
