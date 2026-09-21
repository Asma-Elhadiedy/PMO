
namespace PMO.Application.DTOs.Responses;

public sealed record CommentResponse(
    Guid Id,
    string Content,
    DateTime CreatedAt,
    Guid TaskId,
    string CreatedById
);