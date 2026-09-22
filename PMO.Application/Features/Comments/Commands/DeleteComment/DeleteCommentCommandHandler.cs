

namespace PMO.Application.Features.Comments.Commands.DeleteComment;

public class DeleteCommentCommandHandler(IUnitOfWork _unitOfWork) : IRequestHandler<DeleteCommentCommand, Result<bool>>
{

    public async Task<Result<bool>> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = await _unitOfWork.Repository<Comment>()
            .GetByIdAsync(request.Id, cancellationToken);
        if (comment is null)
            return Result<bool>.Failure("The comment was not found.");

        _unitOfWork.Repository<Comment>().Remove(comment, cancellationToken);
        if (await _unitOfWork.CompleteAsync(cancellationToken) > 0)
            return Result<bool>.Success(true);
        
        return Result<bool>.Failure("Failed to delete the comment.");
    }
}
