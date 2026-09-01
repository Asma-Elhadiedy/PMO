namespace PMO.Application.DTOs.Requests;

internal record CreateProjectRequest(
    string Name,
    string Description,
    DateTime StartDate,
    DateTime EndDate);



