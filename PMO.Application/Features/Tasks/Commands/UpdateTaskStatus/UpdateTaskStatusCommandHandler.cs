
using PMO.Domain.Exceptions;
using System.Net.NetworkInformation;

namespace PMO.Application.Features.Tasks.Commands.UpdateTaskStatus;

public class UpdateTaskStatusCommandHandler(IUnitOfWork _unitOfWork) : IRequestHandler<UpdateTaskStatusCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(UpdateTaskStatusCommand request, CancellationToken cancellationToken)
    {
        var task = await _unitOfWork.Repository<ProjectTask>()
            .GetByIdAsync(request.TaskId, cancellationToken);

        if (task is null)
            return Result<bool>.Failure("Task not found");

        var newStatus = (ETaskStatus)request.NewStatus;
        if (!CanTransitionTo(task.Status, newStatus))
            throw new InvalidTaskStatusTransitionException(task.Status, newStatus);

        task.Status = newStatus;
        if (await _unitOfWork.CompleteAsync(cancellationToken) > 0)
            return Result<bool>.Success(true);

        return Result<bool>.Failure("Failed to update task status");
    }

    private static bool CanTransitionTo(ETaskStatus currentStatus, ETaskStatus newStatus)
    {
        var allowedTransitions = new Dictionary<ETaskStatus, List<ETaskStatus>>
        {
            { ETaskStatus.Todo, [ETaskStatus.InProgress, ETaskStatus.Cancelled ] },
            { ETaskStatus.InProgress, [ETaskStatus.Completed, ETaskStatus.Cancelled ] },
            { ETaskStatus.Completed, [] },
            { ETaskStatus.Cancelled, [] }
        };

        return allowedTransitions.TryGetValue(currentStatus, out var validNextStatuses) && validNextStatuses.Contains(newStatus);
    }
}