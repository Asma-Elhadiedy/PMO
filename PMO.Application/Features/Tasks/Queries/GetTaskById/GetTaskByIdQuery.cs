
namespace PMO.Application.Features.Tasks.Queries.GetTaskById;

internal sealed record GetTaskByIdQuery(Guid Id) : IRequest<TaskResponse>;

