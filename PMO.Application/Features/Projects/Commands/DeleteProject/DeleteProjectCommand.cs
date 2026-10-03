
namespace PMO.Application.Features.Projects.Commands.DeleteProject;

public sealed record DeleteProjectCommand(Guid Id, string DeletedBy) : IRequest<Result<bool>>;
