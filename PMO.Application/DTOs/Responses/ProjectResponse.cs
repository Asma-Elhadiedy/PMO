
namespace PMO.Application.DTOs.Responses;

public sealed record ProjectResponse(
    Guid Id,
    string Name,
    string Description,
    DateTime StartDate,
    DateTime? EndDate,
    string Status);