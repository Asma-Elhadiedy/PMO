

namespace PMO.Application.Features.Comments.Queries.ListComments;

public sealed class GetCommentsQueryHandler(IUnitOfWork _unitOfWork) : IRequestHandler<GetCommentsQuery, Result<IReadOnlyList<CommentResponse>>>
{

    public async Task<Result<IReadOnlyList<CommentResponse>>> Handle(GetCommentsQuery request, CancellationToken cancellationToken)
    {
        var comments = await _unitOfWork.Repository<Comment>()
                .GetAllSelectedAsync(
                c => new CommentResponse(
                c.Id,
                c.Content,
                c.CreatedAt,
                c.TaskId,
                c.CreatedById),
                c => c.TaskId == request.TaskId
            , cancellationToken);

        return Result<IReadOnlyList<CommentResponse>>.Success(comments);
    }
}