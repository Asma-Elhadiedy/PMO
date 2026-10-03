
namespace PMO.Application.DTOs.Requests;

public record UpdateTaskRequest(
    string Name,
    string Description,
    DateTime StartDate,
    DateTime EndDate);