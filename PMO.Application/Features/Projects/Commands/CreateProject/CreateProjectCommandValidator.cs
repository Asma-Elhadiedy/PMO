

namespace PMO.Application.Features.Projects.Commands.CreateProject;

internal class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
{
    public CreateProjectCommandValidator()
    {
        RuleFor(x => x.Request.Name)
            .NotEmpty().WithMessage("Project name is required.")
            .MaximumLength(100).WithMessage("Project name must not exceed 100 characters.");

        RuleFor(x => x.Request.Description)
            .MaximumLength(500).WithMessage("Project description must not exceed 500 characters.");

        RuleFor(x => x.Request.StartDate)
            .LessThanOrEqualTo(x => x.Request.EndDate).WithMessage("Start date must be less than or equal to end date.");


    }
}
