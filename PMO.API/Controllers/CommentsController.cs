
using PMO.Application.Features.Comments.Commands.CreateComment;
using PMO.Application.Features.Comments.Commands.DeleteComment;
using PMO.Application.Features.Comments.Queries.ListComments;

namespace PMO.API.Controllers;

[ApiController]
[Authorize(Roles = ConstRoles.User)]
[Produces("application/json")]
[Route("api/tasks/{taskId:guid}/[controller]")]
[ProducesResponseType(StatusCodes.Status403Forbidden)]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
public class CommentsController(IMediator _mediator, ILogger<CommentsController> _logger) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<Result<IReadOnlyList<CommentResponse>>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(Guid taskId)
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

    [HttpPost]
    [ProducesResponseType<Result<Guid>>(StatusCodes.Status201Created)]
    public async Task<IActionResult> Create(Guid taskId, [FromBody] string Content)
    {
        var commentsResult = await _mediator.Send(new CreateCommentCommand(taskId, Content, CreatedBy: User.Id));
        if (!commentsResult.IsSuccess)
        {
            _logger.LogWarning(commentsResult.Error);
            return NotFound(commentsResult);
        }

        _logger.LogInformation("Comment created successfully");
        return CreatedAtAction(nameof(GetAll), new { taskId }, commentsResult.Data);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _mediator.Send(new DeleteCommentCommand(id, User.Id));
        if (!result.IsSuccess)
        {
            _logger.LogWarning(result.Error);
            return NotFound(result);
        }

        _logger.LogInformation("Comment deleted successfully");
        return Ok(result);
    }
}
