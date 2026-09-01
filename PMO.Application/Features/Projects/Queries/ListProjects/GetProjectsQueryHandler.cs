
namespace PMO.Application.Features.Projects.Queries.ListProjects;

internal sealed class GetProjectsQueryHandler(IUnitOfWork _unitOfWork) : IRequestHandler<GetProjectsQuery, IReadOnlyList<ProjectsResponse>>
{
    public async Task<IReadOnlyList<ProjectsResponse>> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
    {
        var projects = await _unitOfWork.Repository<Project>()
            .GetAllSelectedAsync(p => new ProjectsResponse(
                Id: p.Id,
                Name: p.Name,
                Description: p.Description,
            StartDate: p.StartDate,
            EndDate: p.EndDate,
            Status: p.Status
        ), null, cancellationToken);

        return projects;
    }
}

