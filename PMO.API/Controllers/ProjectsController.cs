using PMO.Application.Features.Projects.Queries.ListProjects;
using PMO.Application.Features.Projects.Queries.GetProjectById;

namespace PMO.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController(ILogger<ProjectsController> _logger, IMediator _mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken ct)
    {
        var projects = await _mediator.Send(new GetProjectsQuery(), ct);

        _logger.LogInformation("Projects fetched successfully");
        return Ok(projects);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var project = await _mediator.Send(new GetProjectByIdQuery(id), ct);

        if (project is null)
        {
            return NotFound();
        }

        _logger.LogInformation("Project fetched successfully");
        return Ok(project);
    }
}
