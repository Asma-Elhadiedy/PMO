
namespace PMO.Application.Features.Tasks.Commands.CreateTask;

internal sealed class CreateTaskCommandHandler(IUnitOfWork _unitOfWork) : IRequestHandler<CreateTaskCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var model = request.Request;
        var task = new ProjectTask
        {
            Name = model.Name,
            Description = model.Description,
            ProjectId = model.ProjectId,
            StartDate = model.StartDate,
            EndDate = model.EndDate
        };

        _unitOfWork.Repository<ProjectTask>().Add(task);
        if (await _unitOfWork.CompleteAsync(cancellationToken) > 0)
            return Result<Guid>.Success(task.Id);
        return Result<Guid>.Failure("Failed to create the task.");
    }
}
