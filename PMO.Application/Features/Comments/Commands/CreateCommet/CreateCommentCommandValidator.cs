

namespace PMO.Application.Features.Comments.Commands.CreateComment;

public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
{
    public CreateCommentCommandValidator(IUnitOfWork _unitOfWork)
    {
        RuleFor(x => x.TaskId)
            .NotEmpty().WithMessage("TaskId is required.");

        RuleFor(x => x.TaskId)
            .MustAsync(async (taskId, ct) =>
                await _unitOfWork.Repository<ProjectTask>().ExistsAsync(taskId, ct))
            .WithMessage("Task Id is invalid.");

        RuleFor(x => x.CreatedBy)
            .MustAsync(async (cmd, createdBy, ct) =>
                 createdBy == await _unitOfWork.Repository<ProjectTask>()
                        .GetItemSelectedAsync(
                            t => t.CreatedById,
                            t => t.Id == cmd.TaskId,
                            ct)
                    )
            .WithMessage("Another user owns this task, you can not create a comment.");


        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Content is required.");
    }
}