

namespace PMO.Application.DTOs.Requests;

public record CreateTaskRequest(
    string Name,
    string Description,
    Guid ProjectId,
    DateTime StartDate,
    DateTime EndDate);