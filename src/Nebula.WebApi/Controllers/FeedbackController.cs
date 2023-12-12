using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Nebula.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FeedbackController : ControllerBase
{
    private readonly IMediator mediator;

    public FeedbackController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [Authorize(Roles = "superAdmin")]
    [Authorize(Roles = "admin")]
    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync(CreateFeedbackCommand command)
        => Ok(await this.mediator.Send(new CreateFeedbackCommand(command.Rating, command.Comment, command.FeedbackDate,
            command.RentalId, command.CustomerId)));

    [Authorize(Roles = "superAdmin")]
    [Authorize(Roles = "admin")]
    [HttpPut("update")]
    public async Task<IActionResult> UpdateAsync(UpdateFeedbackCommand command)
        => Ok(await this.mediator.Send(new UpdateFeedbackCommand(command.Id, command.Rating, command.Comment, command.FeedbackDate,
            command.RentalId, command.CustomerId)));

    [Authorize(Roles = "superAdmin")]
    [Authorize(Roles = "admin")]
    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
        => Ok(await this.mediator.Send(new DeleteFeedbackCommand(id)));

    [AllowAnonymous]
    [HttpGet("get-by-id/{id:long}")]
    public async Task<IActionResult> GetByIdAsync(long id)
        => Ok(await this.mediator.Send(new GetFeedbackQuery() { Id = id }));

    [AllowAnonymous]
    [HttpGet("get-all")]
    public async Task<IActionResult> GetAll()
        => Ok(await this.mediator.Send(new GetAllFeedbacksQuery()));

}
