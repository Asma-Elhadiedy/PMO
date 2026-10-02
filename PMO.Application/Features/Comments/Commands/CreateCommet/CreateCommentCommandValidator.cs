

namespace PMO.Application.Features.Comments.Commands.CreateComment;

public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
{
    public CreateCommentCommandValidator(IUnitOfWork _unitOfWork)
    {
        RuleFor(x => x.Request.TaskId)
            .NotEmpty().WithMessage("TaskId is required.");

        RuleFor(x => x.Request.TaskId)
            .MustAsync(async (taskId, ct) => 
                await _unitOfWork.Repository<ProjectTask>().ExistsAsync(taskId, ct))
            .WithMessage("Task Id is invalid.");


        RuleFor(x => x.Request.Content)
            .NotEmpty().WithMessage("Content is required.");
    }
}