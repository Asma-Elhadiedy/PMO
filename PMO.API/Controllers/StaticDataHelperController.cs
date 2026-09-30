using PMO.Application.Features.StaticData.Queries.TaskStatuses;

namespace PMO.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class StaticDataHelperController(IMediator _mediator, ILogger<StaticDataHelperController> _logger) : ControllerBase
{
    [HttpGet("TaskStatuses")]
    public async Task<IActionResult> GetTaskStatusesData(CancellationToken ct)
    {
        var staticDataResult = await _mediator.Send(new GetTaskStatusesQuery(), ct);
        if (!staticDataResult.IsSuccess)
        {
            _logger.LogWarning(staticDataResult.Error);
            return NotFound(staticDataResult);
        }
        _logger.LogInformation("Static data fetched successfully");
        return Ok(staticDataResult);
    }

}