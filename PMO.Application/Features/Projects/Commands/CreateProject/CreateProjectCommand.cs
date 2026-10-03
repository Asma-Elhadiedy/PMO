
namespace PMO.Application.Features.Projects.Commands.CreateProject;

public sealed record CreateProjectCommand(CreateProjectRequest Request, string CreatedBy) : IRequest<Result<Guid>>;
    