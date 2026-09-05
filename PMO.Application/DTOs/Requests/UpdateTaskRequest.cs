
namespace PMO.Application.DTOs.Requests;

public record UpdateTaskRequest(
    Guid Id,
    string Name,
    string Description,
    int Status,
    Guid ProjectId,
    DateTime StartDate,
    DateTime EndDate);