using PMO.Application.Features.Tasks.Commands.CreateTask;
using PMO.Application.Features.Tasks.Queries.GetTaskById;
using PMO.Application.Features.Tasks.Queries.ListTasks;

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
    public async Task<IActionResult> GetById([FromRoute]Guid id, CancellationToken ct)
    {
        var task = await _mediator.Send(new GetTaskByIdQuery(id), ct);

        if (task is null)
        {
            return NotFound();
        }

        _logger.LogInformation("Task fetched successfully");
        return Ok(task);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTaskRequest request, CancellationToken ct)
    {
        var taskId = await _mediator.Send(new CreateTaskCommand(request), ct);

        _logger.LogInformation("Task created successfully");
        return CreatedAtAction(nameof(GetById), new { id = taskId }, new { taskId });
    }
}
