

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

        RuleFor(x => x.Request.ProjectId)
            .NotEmpty().WithMessage("Project ID is required.");

        RuleFor(x => x.Request.OwnerId)
            .NotEmpty().WithMessage("Owner ID is required.");

        RuleFor(x => x.Request.ProjectId)
            .MustAsync(async (projectId, ct) =>
                await _unitOfWork.Repository<Project>().ExistsAsync(projectId, ct))
            .WithMessage("Project ID is invalid.");

        RuleFor(x => x.Request.OwnerId)
            .MustAsync(async (ownerId, ct) =>
            {
                return await _identityService.UserExistsAsync(ownerId);
            })
            .WithMessage("Owner ID is invalid.");
    }
}
