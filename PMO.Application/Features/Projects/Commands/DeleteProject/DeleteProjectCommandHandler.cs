
namespace PMO.Application.Features.Projects.Commands.DeleteProject;

internal sealed class DeleteProjectCommandHandler(IUnitOfWork _unitOfWork) : IRequestHandler<DeleteProjectCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _unitOfWork.Repository<Project>()
            .GetByIdAsync(request.Id, cancellationToken);
        if (project == null)
            return Result<bool>.Failure("Project not found");

        _unitOfWork.Repository<Project>().Remove(project, cancellationToken);
        if (await _unitOfWork.CompleteAsync(cancellationToken) > 0)
            return Result<bool>.Success(true);
        return Result<bool>.Failure("Failed to delete project");
    }
}
