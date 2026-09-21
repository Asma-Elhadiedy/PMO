
namespace PMO.Application.DTOs.Requests;


public record UpdateProjectRequest(
    Guid Id,
    string Name,
    string Description,
    DateTime StartDate,
    DateTime EndDate);