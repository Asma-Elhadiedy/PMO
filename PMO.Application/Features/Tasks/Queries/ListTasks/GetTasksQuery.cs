
namespace PMO.Application.Features.Tasks.Queries.ListTasks;

public sealed record GetTasksQuery : IRequest<IReadOnlyList<TaskResponse>>;
