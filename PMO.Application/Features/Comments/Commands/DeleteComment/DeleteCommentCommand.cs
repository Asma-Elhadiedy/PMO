

namespace PMO.Application.Features.Comments.Commands.DeleteComment;

public sealed record DeleteCommentCommand(Guid Id) : IRequest<Result<bool>>;
