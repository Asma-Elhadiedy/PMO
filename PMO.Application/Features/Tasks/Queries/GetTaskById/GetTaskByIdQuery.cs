
namespace PMO.Application.Features.Tasks.Queries.GetTaskById;

public sealed record GetTaskByIdQuery(Guid Id) : IRequest<TaskResponse>;

