
namespace PMO.Application.Features.Tasks.Commands.UpdateTask;

internal sealed class UpdateTaskCommandHandler(IUnitOfWork _unitOfWork) : IRequestHandler<UpdateTaskCommand, bool>
{
    public async Task<bool> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        var model = request.Request;
        var task = await _unitOfWork.Repository<ProjectTask>()
            .GetByIdAsync(model.Id, cancellationToken);


        task.Name = model.Name;
        task.Description = model.Description;
        task.Status = (ETaskStatus)model.Status;
        task.StartDate = model.StartDate;
        task.EndDate = model.EndDate;

        return await _unitOfWork.CompleteAsync(cancellationToken) > 0;
    }
}
