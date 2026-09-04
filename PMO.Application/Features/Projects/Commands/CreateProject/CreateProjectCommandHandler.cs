
namespace PMO.Application.Features.Projects.Commands.CreateProject;

internal sealed class CreateProjectCommandHandler(IUnitOfWork _unitOfWork) : IRequestHandler<CreateProjectCommand, Guid>
{
    public async Task<Guid> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = new Project
        {
            Name = request.Request.Name,
            Description = request.Request.Description,
            StartDate = request.Request.StartDate,
            EndDate = request.Request.EndDate
        };

        _unitOfWork.Repository<Project>().Add(project);
        await _unitOfWork.CompleteAsync(cancellationToken);

        return project.Id;
    }
}
