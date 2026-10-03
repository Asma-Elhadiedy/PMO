
namespace PMO.Application.Features.Tasks.Commands.UpdateTaskStatus;

public class UpdateTaskStatusValidator : AbstractValidator<UpdateTaskStatusCommand>
{
    public UpdateTaskStatusValidator()
    {
        RuleFor(x => x.TaskId)
            .NotEmpty().WithMessage("TaskId is required");

        RuleFor(x => x.NewStatus)
            .IsInEnum().WithMessage("New Status must be a valid status");
    }
}
