

namespace PMO.Application.Features.Comments.Commands.CreateComment;

public record CreateCommentCommand(Guid TaskId, string Content, string CreatedBy) : IRequest<Result<Guid>>;
