
namespace PMO.Application.Features.Tasks.Commands.UpdateTask;

internal sealed class UpdateTaskCommandHandler(IUnitOfWork _unitOfWork) : IRequestHandler<UpdateTaskCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        var model = request.Request;
        var task = await _unitOfWork.Repository<ProjectTask>()
            .GetByIdAsync(model.Id, cancellationToken);


        task.Name = model.Name;
        task.Description = model.Description;
        task.Status = (ETaskStatus)model.Status;
        task.StartDate = model.StartDate;
        task.EndDate = model.EndDate;
        task.ProjectId = model.ProjectId;

        if (await _unitOfWork.CompleteAsync(cancellationToken) > 0)
            return Result<bool>.Success(true);

        return Result<bool>.Failure("Failed to update task");
    }
}
