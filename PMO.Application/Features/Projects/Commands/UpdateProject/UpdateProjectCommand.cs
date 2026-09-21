
namespace PMO.Application.Features.Projects.Commands.UpdateProject;

public sealed record UpdateProjectCommand(UpdateProjectRequest Request) : IRequest<Result<bool>>;
