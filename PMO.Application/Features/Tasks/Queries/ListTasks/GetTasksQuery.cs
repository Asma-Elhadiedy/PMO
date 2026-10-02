
namespace PMO.Application.Features.Tasks.Queries.ListTasks;

public sealed record GetTasksQuery(Guid ProjectId) : IRequest<Result<IReadOnlyList<TaskResponse>>>;
