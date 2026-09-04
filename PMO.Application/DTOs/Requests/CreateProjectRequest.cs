namespace PMO.Application.DTOs.Requests;

public record CreateProjectRequest(
    string Name,
    string Description,
    DateTime StartDate,
    DateTime EndDate);



