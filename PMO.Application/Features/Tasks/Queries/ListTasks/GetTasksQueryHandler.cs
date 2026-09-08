
namespace PMO.Application.Features.Tasks.Queries.ListTasks;

internal sealed class GetTasksQueryHandler(IUnitOfWork _unitOfWork) : IRequestHandler<GetTasksQuery, Result<IReadOnlyList<TaskResponse>>>
{
    public async Task<Result<IReadOnlyList<TaskResponse>>> Handle(GetTasksQuery request, CancellationToken cancellationToken)
    {
        var tasks = await _unitOfWork.Repository<ProjectTask>()
               .GetAllSelectedAsync(t => new TaskResponse(
                   Id: t.Id,
                   Name: t.Name,
                   Description: t.Description,
                   StartDate: t.StartDate,
                   EndDate: t.EndDate,
                   Status: t.Status.ToString()
        ), null, cancellationToken);

        return Result<IReadOnlyList<TaskResponse>>.Success(tasks);
    }

}
