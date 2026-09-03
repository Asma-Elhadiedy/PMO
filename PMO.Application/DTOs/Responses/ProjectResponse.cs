
namespace PMO.Application.DTOs.Responses;

internal sealed record ProjectResponse(
    Guid Id,
    string Name,
    string Description,
    DateTime StartDate,
    DateTime? EndDate,
    EProjectStatus Status);