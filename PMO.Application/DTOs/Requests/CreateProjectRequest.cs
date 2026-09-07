using System.ComponentModel;

namespace PMO.Application.DTOs.Requests;

public record CreateProjectRequest(
    [property: DefaultValue("Project Name")] string Name,
    [property: DefaultValue("Project Description")] string Description,
    DateTime StartDate,
    DateTime EndDate);

