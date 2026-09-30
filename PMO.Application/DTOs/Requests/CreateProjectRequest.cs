using System.ComponentModel;

namespace PMO.Application.DTOs.Requests;

public record CreateProjectRequest(
    [property: DefaultValue("Project Name")] string Name,
    [property: DefaultValue("Project Description")] string Description,
    [property: DefaultValue("9be94bdd-f48c-446b-9934-86ee3fe03729")]
    string OwnerId,
    DateTime StartDate,
    DateTime EndDate);

