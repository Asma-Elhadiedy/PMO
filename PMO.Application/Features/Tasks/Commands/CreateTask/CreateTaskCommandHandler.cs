
namespace PMO.Application.Features.Tasks.Commands.CreateTask;

internal sealed class CreateTaskCommandHandler(IUnitOfWork _unitOfWork) : IRequestHandler<CreateTaskCommand, Guid>
{
    public async Task<Guid> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
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
        await _unitOfWork.CompleteAsync(cancellationToken);

        return task.Id;
    }
}
