
namespace PMO.Application.DTOs.Requests;

public record UpdateTaskRequest(
    Guid Id,
    string Name,
    string Description,
    DateTime StartDate,
    DateTime EndDate);