
namespace PMO.Application.Features.Projects.Queries.GetProjectById;

public sealed record GetProjectByIdQuery(Guid Id) : IRequest<ProjectResponse>;
