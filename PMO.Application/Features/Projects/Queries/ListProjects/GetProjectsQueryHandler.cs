
namespace PMO.Application.Features.Projects.Queries.ListProjects;

internal sealed class GetProjectsQueryHandler(IUnitOfWork _unitOfWork) : IRequestHandler<GetProjectsQuery, IReadOnlyList<ProjectResponse>>
{
    public async Task<IReadOnlyList<ProjectResponse>> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
    {
        var projects = await _unitOfWork.Repository<Project>()
            .GetAllSelectedAsync(p => new ProjectResponse(
                Id: p.Id,
                Name: p.Name,
                Description: p.Description,
                StartDate: p.StartDate,
                EndDate: p.EndDate,
                Status: p.Status.ToString()
        ), null, cancellationToken);

        return projects;
    }
}

