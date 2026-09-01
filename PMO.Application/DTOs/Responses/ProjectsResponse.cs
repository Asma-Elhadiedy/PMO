
namespace PMO.Application.DTOs.Responses;

internal sealed record ProjectsResponse(
    Guid Id,
    string Name,
    string Description,
    DateTime StartDate,
    DateTime EndDate,
    EProjectStatus Status);