namespace PMO.Application.DTOs.Responses;

internal sealed record TaskResponse(
    Guid Id,
    string Name,
    string Description,
    ETaskStatus Status);