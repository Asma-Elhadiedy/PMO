

namespace PMO.Application.Features.Comments.Commands.CreateComment;

public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
{
    public CreateCommentCommandValidator()
    {
        RuleFor(x => x.Request.TaskId)
            .NotEmpty().WithMessage("TaskId is required.");

        RuleFor(x => x.Request.Content)
            .NotEmpty().WithMessage("Content is required.");
    }
}