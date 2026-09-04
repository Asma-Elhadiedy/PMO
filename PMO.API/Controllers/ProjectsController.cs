using PMO.Application.Features.Projects.Queries.ListProjects;
using PMO.Application.Features.Projects.Queries.GetProjectById;
using PMO.Application.Features.Projects.Commands.CreateProject;

namespace PMO.API.Controllers;

[ApiController]
[Route("api/[controller]")]
//[Produces("application/json")]
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
    public async Task<IActionResult> GetById([FromRoute]Guid id, CancellationToken ct)
    {
        var project = await _mediator.Send(new GetProjectByIdQuery(id), ct);

        if (project is null)
        {
            return NotFound();
        }

        _logger.LogInformation("Project fetched successfully");
        return Ok(project);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody]CreateProjectRequest request, CancellationToken ct)
    {
        var projectId = await _mediator.Send(new CreateProjectCommand(request), ct);

        _logger.LogInformation("Project created successfully");
        return CreatedAtAction(nameof(GetById), new { id = projectId }, new { projectId});
    }
}
