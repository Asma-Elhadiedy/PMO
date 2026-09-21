

namespace PMO.Application.Features.Comments.Commands.CreateComment;

internal class CreateCommentCommandHandler(IUnitOfWork _unitOfWork) : IRequestHandler<CreateCommentCommand, Result<Guid>>
{
    public Task<Result<Guid>> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        _unitOfWork.Repository<Comment>().Add(new Comment
        {
            Content = request.Request.Content,
            TaskId = request.Request.TaskId,
            CreatedAt = DateTime.UtcNow,
            CreatedById = string.Empty 
        });

        return _unitOfWork.CompleteAsync(cancellationToken)
            .ContinueWith(t =>
            {
                if (t.IsCompletedSuccessfully)
                {
                    return Result<Guid>.Success(request.Request.TaskId);
                }
                else
                {
                    return Result<Guid>.Failure("Failed to create comment.");
                }
            }, cancellationToken);
    }
}
