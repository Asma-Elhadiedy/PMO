

namespace PMO.Application.Features.Comments.Queries.ListComments;

public sealed record GetCommentsQuery(Guid TaskId) : IRequest<Result<IReadOnlyList<CommentResponse>>>;
