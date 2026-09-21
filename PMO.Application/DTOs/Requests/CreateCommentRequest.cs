
namespace PMO.Application.DTOs.Requests;

public record CreateCommentRequest(
    Guid TaskId,
    string Content
    );