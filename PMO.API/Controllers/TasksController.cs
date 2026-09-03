using PMO.Application.Features.Tasks.Queries.ListTasks;
using PMO.Application.Features.Tasks.Queries.GetTaskById;

namespace PMO.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController(ILogger<TasksController> _logger, IMediator _mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken ct)
    {
        var tasks = await _mediator.Send(new GetTasksQuery(), ct);

        _logger.LogInformation("Tasks fetched successfully");
        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var task = await _mediator.Send(new GetTaskByIdQuery(id), ct);

        if (task is null)
        {
            return NotFound();
        }

        _logger.LogInformation("Task fetched successfully");
        return Ok(task);
    }
}
