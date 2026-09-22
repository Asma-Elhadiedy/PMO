
namespace PMO.Application.Features.Projects.Commands.CreateProject;

internal sealed class CreateProjectCommandHandler(IUnitOfWork _unitOfWork) : IRequestHandler<CreateProjectCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var model = request.Request;
        var project = new Project
        {
            Name = model.Name,
            Description = model.Description,
            StartDate = model.StartDate,
            EndDate = model.EndDate
        };

        _unitOfWork.Repository<Project>().Add(project);
        if (await _unitOfWork.CompleteAsync(cancellationToken) > 0)
            return Result<Guid>.Success(project.Id);
        return Result<Guid>.Failure("Failed to create the project.");
    }
}
