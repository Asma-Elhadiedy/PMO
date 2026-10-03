
namespace PMO.Application.DTOs.Requests;

public record UpdateProjectRequest(
    string Name,
    string Description,
    DateTime StartDate,
    DateTime EndDate);