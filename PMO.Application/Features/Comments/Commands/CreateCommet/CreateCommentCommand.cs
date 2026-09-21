

namespace PMO.Application.Features.Comments.Commands.CreateComment;

public record CreateCommentCommand(CreateCommentRequest Request) : IRequest<Result<Guid>>;
