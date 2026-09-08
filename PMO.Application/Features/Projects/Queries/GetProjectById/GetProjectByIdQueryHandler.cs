
namespace PMO.Application.Features.Projects.Queries.GetProjectById;

internal sealed class GetProjectByIdQueryHandler(IUnitOfWork _unitOfWork) : IRequestHandler<GetProjectByIdQuery, Result<ProjectResponse>>
{
    public async Task<Result<ProjectResponse>> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        var project = await _unitOfWork.Repository<Project>()
            .GetByIdAsync(request.Id, cancellationToken);

        if (project is null)
            return Result<ProjectResponse>.Failure($"Project with Id {request.Id} not found.");

        return Result<ProjectResponse>.Success(
            new ProjectResponse(
                project.Id,
                project.Name,
                project.Description,
                project.StartDate,
                project.EndDate,
                project.Status.ToString())
                );
    }
}