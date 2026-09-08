
namespace PMO.Application.Features.Tasks.Queries.GetTaskById;

internal sealed class GetTaskByIdQueryHandler(IUnitOfWork _unitOfWork) : IRequestHandler<GetTaskByIdQuery, Result<TaskResponse>>
{
    public async Task<Result<TaskResponse>> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
    {
        var task = await _unitOfWork.Repository<ProjectTask>().GetByIdAsync(request.Id, cancellationToken);
        if (task is null)
            return Result<TaskResponse>.Failure("Task not found");

        return Result<TaskResponse>.Success(new TaskResponse(
            task.Id,
            task.Name,
            task.Description,
            task.StartDate,
            task.EndDate,
            task.Status.ToString()));
    }

}
