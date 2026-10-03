
namespace PMO.Application.Features.Tasks.Commands.CreateTask;

internal class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
{
    public CreateTaskCommandValidator(IIdentityService _identityService, IUnitOfWork _unitOfWork)
    {
        RuleFor(x => x.Request.Name)
            .NotEmpty().WithMessage("Task name is required.")
            .MaximumLength(100).WithMessage("Task name must not exceed 100 characters.");

        RuleFor(x => x.Request.Description)
            .MaximumLength(500).WithMessage("Task description must not exceed 500 characters.");

        RuleFor(x => x.Request.StartDate)
            .LessThanOrEqualTo(x => x.Request.EndDate).WithMessage("Start date must be less than or equal to end date.");

        RuleFor(x => x.ProjectId)
            .NotEmpty().WithMessage("Project ID is required.");

        RuleFor(x => x.ProjectId)
            .MustAsync(async (projectId, ct) =>
                await _unitOfWork.Repository<Project>().ExistsAsync(projectId, ct))
            .WithMessage("Project ID is invalid.");

        RuleFor(x => x.CreatedBy)
            .MustAsync(async (createdById, ct) =>
            {
                return await _identityService.UserExistsAsync(createdById);
            })
            .WithMessage("Created By ID is invalid.");

        RuleFor(x => x.CreatedBy)
            .MustAsync(async (cmd, createdById, ct) =>
                createdById == await _unitOfWork.Repository<Project>()
                    .GetItemSelectedAsync(
                        p => p.CreatedById,
                        p => p.Id == cmd.ProjectId,
                        ct)
            ).WithMessage("You are not authorized to add tasks to this project.");
    }
}
