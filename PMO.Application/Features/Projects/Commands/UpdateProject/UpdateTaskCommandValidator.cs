

namespace PMO.Application.Features.Projects.Commands.UpdateProject;

internal class UpdateTaskCommandValidator : AbstractValidator<UpdateProjectCommand>
{
    public UpdateTaskCommandValidator(IUnitOfWork _unitOfWork)
    {
        RuleFor(x => x.Request.Name)
            .NotEmpty().WithMessage("Project name is required.")
            .MaximumLength(100).WithMessage("Project name must not exceed 100 characters.");

        RuleFor(x => x.Request.Description)
            .MaximumLength(500).WithMessage("Project description must not exceed 500 characters.");

        RuleFor(x => x.Request.StartDate)
            .LessThanOrEqualTo(x => x.Request.EndDate).WithMessage("Start date must be less than or equal to end date.");

        RuleFor(x => x.UpdatedBy)
            .MustAsync(async (cmd, updatedById, cancellation) =>
            
                updatedById == await _unitOfWork.Repository<Project>()
                    .GetItemSelectedAsync(
                        p => p.CreatedById,
                        p => p.Id == cmd.Id,
                        cancellation)
            ).WithMessage("You are not authorized to delete this project.");
    }
}
