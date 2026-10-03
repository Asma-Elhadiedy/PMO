

namespace PMO.Application.DTOs.Requests;

public record CreateTaskRequest(
    string Name,
    string Description,
    DateTime StartDate,
    DateTime EndDate);