
namespace PMO.Application.Features.Projects.Queries.ListProjects;

public sealed class GetProjectsQuery : IRequest<Result<IReadOnlyList<ProjectResponse>>>;
