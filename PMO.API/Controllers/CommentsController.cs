

using PMO.Application.Features.Comments.Queries.ListComments;

namespace PMO.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class CommentsController(IMediator _mediator, ILogger<CommentsController> _logger) : ControllerBase
{
    [HttpGet("{taskId}")]
    [ProducesResponseType<Result<IReadOnlyList<CommentResponse>>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(Guid taskId)
    {
        var commentsResult = await _mediator.Send(new GetCommentsQuery(taskId));
        if (!commentsResult.IsSuccess)
        {
            _logger.LogWarning(commentsResult.Error);
            return NotFound(commentsResult);
        }

        _logger.LogInformation("Comments fetched successfully");
        return Ok(commentsResult);
    }
}
