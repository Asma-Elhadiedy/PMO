
namespace PMO.Application.Features.Tasks.Commands.DeleteTask;

internal sealed class DeleteTaskCommandHandler(IUnitOfWork _unitOfWork) : IRequestHandler<DeleteTaskCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _unitOfWork.Repository<ProjectTask>()
            .GetByIdAsync(request.Id, cancellationToken);
        if (task == null)
            return Result<bool>.Failure("Task not found");

        if(task.CreatedById != request.DeletedById)
            return Result<bool>.Failure("You are not authorized to delete this task.");

        _unitOfWork.Repository<ProjectTask>().Remove(task, cancellationToken);
        if (await _unitOfWork.CompleteAsync(cancellationToken) > 0)
            return Result<bool>.Success(true);
        return Result<bool>.Failure("Failed to delete task");
    }
}
