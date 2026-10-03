
namespace PMO.Application.Features.Projects.Commands.UpdateProject;

public sealed record UpdateProjectCommand(Guid Id, string UpdatedBy, UpdateProjectRequest Request) : IRequest<Result<bool>>;
