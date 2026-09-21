
namespace PMO.Application.Features.Projects.Commands.UpdateProject;

internal sealed class UpdateProjectCommandHandler(IUnitOfWork _unitOfWork) : IRequestHandler<UpdateProjectCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var model = request.Request;
        var project = await _unitOfWork.Repository<ProjectTask>()
            .GetByIdAsync(model.Id, cancellationToken);


        project.Name = model.Name;
        project.Description = model.Description;
        project.StartDate = model.StartDate;
        project.EndDate = model.EndDate;

        if (await _unitOfWork.CompleteAsync(cancellationToken) > 0)
            return Result<bool>.Success(true);

        return Result<bool>.Failure("Failed to update task");
    }
}
