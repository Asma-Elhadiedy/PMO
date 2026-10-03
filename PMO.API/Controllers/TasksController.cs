using PMO.Application.Features.Comments.Queries.ListComments;
using PMO.Application.Features.Tasks.Commands.CreateTask;
using PMO.Application.Features.Tasks.Commands.DeleteTask;
using PMO.Application.Features.Tasks.Commands.UpdateTask;
using PMO.Application.Features.Tasks.Commands.UpdateTaskStatus;
using PMO.Application.Features.Tasks.Queries.GetTaskById;
using PMO.Application.Features.Tasks.Queries.ListTasks;
using PMO.Domain.Enums;

namespace PMO.API.Controllers;

[ApiController]
[Route("api/projects/{projectId:guid}/[controller]")]
[Authorize(Roles = ConstRoles.User)]
[Produces("application/json")]
[ProducesResponseType(StatusCodes.Status403Forbidden)]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
public class TasksController(ILogger<TasksController> _logger, IMediator _mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<Result<IReadOnlyList<TaskResponse>>>(StatusCodes.Status200OK)]
    [ProducesResponseType<Result<IReadOnlyList<TaskResponse>>>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(Guid projectId, CancellationToken ct)
    {
        var tasksResult = await _mediator.Send(new GetTasksQuery(projectId), ct);

        if (!tasksResult.IsSuccess)
        {
            _logger.LogWarning(tasksResult.Error);
            return NotFound(tasksResult);
        }

        _logger.LogInformation("Tasks fetched successfully");
        return Ok(tasksResult);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType<Result<TaskResponse>>(StatusCodes.Status200OK)]
    [ProducesResponseType<Result<TaskResponse>>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken ct)
    {
        var taskResult = await _mediator.Send(new GetTaskByIdQuery(id), ct);

        if (!taskResult.IsSuccess)
        {
            _logger.LogWarning(taskResult.Error);
            return NotFound(taskResult);
        }

        _logger.LogInformation("Task fetched successfully");
        return Ok(taskResult);
    }


    [HttpPost]
    [ProducesResponseType<Result<Guid>>(StatusCodes.Status201Created)]
    [ProducesResponseType<Result<Guid>>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(Guid projectId, [FromBody] CreateTaskRequest request, CancellationToken ct)
    {
        var creationResult = await _mediator.Send(new CreateTaskCommand(request, projectId, User.Id), ct);

        if (!creationResult.IsSuccess)
        {
            _logger.LogWarning(creationResult.Error);
            return BadRequest(creationResult);
        }

        _logger.LogInformation("Task created successfully");
        return CreatedAtAction(nameof(GetById), new { projectId, id = creationResult.Data }, new { taskId = creationResult.Data });
    }


    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<Result<bool>>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(Guid id, Guid projectId, [FromBody] UpdateTaskRequest request, CancellationToken ct)
    {
        var updateResult = await _mediator.Send(new UpdateTaskCommand(request, id, projectId, User.Id), ct);
        if (!updateResult.IsSuccess)
        {
            _logger.LogWarning(updateResult.Error);
            return BadRequest(updateResult);
        }

        _logger.LogInformation("Task updated successfully");
        return NoContent();
    }

    [HttpPut("{id:guid}/status")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<Result<bool>>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateStatus(Guid id, int newStatus, CancellationToken ct)
    {

        var updateResult = await _mediator.Send(new UpdateTaskStatusCommand(id, (ETaskStatus)newStatus), ct);
        if (!updateResult.IsSuccess)
        {
            _logger.LogWarning(updateResult.Error);
            return BadRequest(updateResult);
        }

        _logger.LogInformation("Task updated successfully");
        return NoContent();
    }


    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<Result<bool>>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken ct)
    {
        var deleteResult = await _mediator.Send(new DeleteTaskCommand(id, User.Id), ct);

        if (!deleteResult.IsSuccess)
        {
            _logger.LogWarning(deleteResult.Error);
            return BadRequest(deleteResult);
        }

        _logger.LogInformation("Task deleted successfully");
        return NoContent();
    }
}
