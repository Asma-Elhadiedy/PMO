
namespace PMO.Application.Features.Projects.Queries.GetProjectById;

internal sealed class GetProjectByIdQueryHandler(IUnitOfWork _unitOfWork) : IRequestHandler<GetProjectByIdQuery, ProjectResponse>
{
    public async Task<ProjectResponse> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        var project = await _unitOfWork.Repository<Project>().GetByIdAsync(request.Id, cancellationToken);
        return new ProjectResponse(
            project.Id, 
            project.Name, 
            project.Description,
            project.StartDate,
            project.EndDate, 
            project.Status.ToString());
    }
}