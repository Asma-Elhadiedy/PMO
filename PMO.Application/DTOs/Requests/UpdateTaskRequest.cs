
namespace PMO.Application.DTOs.Requests;

internal record UpdateTaskRequest(
    string Name,
    string Description,
    DateTime StartDate,
    DateTime EndDate);