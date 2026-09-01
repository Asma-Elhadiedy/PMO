
namespace PMO.Application.Features.Tasks.Queries.ListTasks;

internal sealed class GetTasksQueryHandler : IRequestHandler<GetTasksQuery, IReadOnlyList<TaskResponse>>
{
    public Task<IReadOnlyList<TaskResponse>> Handle(GetTasksQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
     
}
