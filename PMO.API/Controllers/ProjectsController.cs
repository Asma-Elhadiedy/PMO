using PMO.Application.Features.Projects.Queries.ListProjects;
using PMO.Application.Features.Projects.Queries.GetProjectById;
using PMO.Application.Features.Projects.Commands.CreateProject;
using PMO.Application.Features.Projects.Commands.UpdateProject;
using PMO.Application.Features.Projects.Commands.DeleteProject;


namespace PMO.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ProjectsController(ILogger<ProjectsController> _logger, IMediator _mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<Result<IReadOnlyList<ProjectResponse>>>(StatusCodes.Status200OK)]
    [ProducesResponseType<Result<IReadOnlyList<ProjectResponse>>>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(CancellationToken ct)
    {
        var projectsResult = await _mediator.Send(new GetProjectsQuery(), ct);
        if (!projectsResult.IsSuccess)
        {
            _logger.LogWarning(projectsResult.Error);
            return NotFound(projectsResult);
        }

        _logger.LogInformation("Projects fetched successfully");
        return Ok(projectsResult);
    }

    [HttpGet("{id}")]
    [ProducesResponseType<Result<ProjectResponse>>(StatusCodes.Status200OK)]
    [ProducesResponseType<Result<ProjectResponse>>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute]Guid id, CancellationToken ct)
    {
        var projectResult = await _mediator.Send(new GetProjectByIdQuery(id), ct);

        if (!projectResult.IsSuccess)
        {
            _logger.LogWarning(projectResult.Error);
            return NotFound(projectResult);
        }

        _logger.LogInformation("Project fetched successfully");
        return Ok(projectResult);
    }

    [HttpPost]
    [ProducesResponseType<Result<Guid>>(StatusCodes.Status201Created)]
    [ProducesResponseType<Result<Guid>>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody]CreateProjectRequest request, CancellationToken ct)
    {
        var projectResult = await _mediator.Send(new CreateProjectCommand(request), ct);

        if (!projectResult.IsSuccess)
        {
            _logger.LogWarning(projectResult.Error);
            return BadRequest(projectResult);
        }

        _logger.LogInformation("Project created successfully");
        return CreatedAtAction(nameof(GetById), new { id = projectResult.Data }, new { projectId = projectResult.Data });
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<Result<bool>>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update([FromBody] UpdateProjectRequest request, CancellationToken ct)
    {
        var updateResult = await _mediator.Send(new UpdateProjectCommand(request), ct);
        if (!updateResult.IsSuccess)
        {
            _logger.LogWarning(updateResult.Error);
            return BadRequest(updateResult);
        }
        _logger.LogInformation("Project updated successfully");
        return NoContent();
    }


    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<Result<bool>>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete([FromRoute]Guid id, CancellationToken ct)
    {
        var deleteResult = await _mediator.Send(new DeleteProjectCommand(id), ct);
        if (!deleteResult.IsSuccess)
        {
            _logger.LogWarning(deleteResult.Error);
            return BadRequest(deleteResult);
        }
        _logger.LogInformation("Project deleted successfully");
        return NoContent();
    }

}
