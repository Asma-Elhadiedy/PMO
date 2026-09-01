
namespace PMO.Application.Features.Tasks.Queries.ListTasks;

internal sealed record GetTasksQuery : IRequest<IReadOnlyList<TaskResponse>>;
