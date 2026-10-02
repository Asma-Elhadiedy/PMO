

namespace PMO.Application.Features.Comments.Commands.DeleteComment;

public sealed record DeleteCommentCommand(Guid Id, string DeletedById) : IRequest<Result<bool>>;
