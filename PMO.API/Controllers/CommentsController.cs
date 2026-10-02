
using PMO.Application.Features.Comments.Commands.DeleteComment;
using PMO.Application.Features.Comments.Queries.ListComments;

namespace PMO.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = ConstRoles.User)]
[Produces("application/json")]
[ProducesResponseType(StatusCodes.Status403Forbidden)]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
public class CommentsController(IMediator _mediator, ILogger<CommentsController> _logger) : ControllerBase
{
    [HttpGet("{taskId:guid}")]
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
