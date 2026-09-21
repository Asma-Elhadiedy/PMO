
namespace PMO.Application.Features.Tasks.Commands.UpdateTaskStatus;

public class UpdateTaskStatusCommandHandler : IRequestHandler<UpdateTaskStatusCommand, Result<bool>>
{
    public Task<Result<bool>> Handle(UpdateTaskStatusCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}