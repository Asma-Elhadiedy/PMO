
namespace PMO.Application.Features.Projects.Commands.CreateProject;

internal sealed class CreateProjectCommandHandler(IUnitOfWork _unitOfWork) : IRequestHandler<CreateProjectCommand, bool>
{
    public async Task<bool> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        _unitOfWork.Repository<Project>().Add(new Project
        {
            Name = request.Request.Name,
            Description = request.Request.Description,
            StartDate = request.Request.StartDate,
            EndDate = request.Request.EndDate
        });

        return await _unitOfWork.CompleteAsync(cancellationToken) > 0;
    }
}
