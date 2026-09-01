
namespace PMO.Application.Features.Projects.Commands.CreateProject;

internal sealed record CreateProjectCommand(CreateProjectRequest Request) : IRequest<bool>;
