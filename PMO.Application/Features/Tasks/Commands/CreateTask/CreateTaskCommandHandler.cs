
namespace PMO.Application.Features.Tasks.Commands.CreateTask;

internal sealed class CreateTaskCommandHandler(IUnitOfWork _unitOfWork) : IRequestHandler<CreateTaskCommand, Guid>
{
    public async Task<Guid> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var task= new ProjectTask
        {
            Name = request.Request.Name,
            Description = request.Request.Description,
            ProjectId = request.Request.ProjectId,
            StartDate = request.Request.StartDate,
            EndDate = request.Request.EndDate
        };

        _unitOfWork.Repository<ProjectTask>().Add(task);
        await _unitOfWork.CompleteAsync(cancellationToken);

        return task.Id;
    }
}
