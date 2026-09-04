
namespace PMO.Application.Features.Tasks.Queries.GetTaskById;

internal sealed class GetTaskByIdQueryHandler(IUnitOfWork _unitOfWork) : IRequestHandler<GetTaskByIdQuery, TaskResponse>
{
    public async Task<TaskResponse> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
    {
        var task = await _unitOfWork.Repository<ProjectTask>().GetByIdAsync(request.Id, cancellationToken);
        return new TaskResponse(
            task.Id, 
            task.Name, 
            task.Description, 
            task.StartDate,
            task.EndDate,
            task.Status.ToString());
    }

}
