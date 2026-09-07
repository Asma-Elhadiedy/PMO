
namespace PMO.Application.Features.Tasks.Commands.DeleteTask;

internal sealed class DeleteTaskCommandHandler(IUnitOfWork _unitOfWork) : IRequestHandler<DeleteTaskCommand, bool>
{
    public async Task<bool> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _unitOfWork.Repository<ProjectTask>()
            .GetByIdAsync(request.Id, cancellationToken);
        if (task == null)
            return false;

        _unitOfWork.Repository<ProjectTask>().Remove(task, cancellationToken);
        return await _unitOfWork.CompleteAsync(cancellationToken) > 0;
    }
}
