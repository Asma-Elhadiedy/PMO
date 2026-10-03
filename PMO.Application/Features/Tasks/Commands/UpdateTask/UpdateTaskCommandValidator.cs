

namespace PMO.Application.Features.Tasks.Commands.UpdateTask;

internal class UpdateTaskCommandValidator : AbstractValidator<UpdateTaskCommand>
{
    public UpdateTaskCommandValidator(IUnitOfWork _unitOfWork)
    {
        RuleFor(x => x.Request.Name)
            .NotEmpty().WithMessage("Task name is required.")
            .MaximumLength(100).WithMessage("Task name must not exceed 100 characters.");

        RuleFor(x => x.Request.Description)
            .MaximumLength(500).WithMessage("Task description must not exceed 500 characters.");

        RuleFor(x => x.Request.StartDate)
            .LessThanOrEqualTo(x => x.Request.EndDate).WithMessage("Start date must be less than or equal to end date.");

        RuleFor(x => x.UpdatedBy)
            .MustAsync(async (cmd, updatedById, ct) =>
                updatedById == await _unitOfWork.Repository<ProjectTask>()
                    .GetItemSelectedAsync(
                            p => p.CreatedById,
                            p => p.Id == cmd.TaskId && p.ProjectId == cmd.ProjectId,
                            ct)
            ).WithMessage("You are not authorized to update this task.");
    }
}
