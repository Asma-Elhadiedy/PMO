namespace PMO.Application.DTOs.Responses;

public sealed record TaskResponse(
    Guid Id,
    string Name,
    string Description,
    DateTime StartDate,
    DateTime? EndDate,   
    ETaskStatus Status);