using Nebula.Application.Queries.Attachments;

namespace Nebula.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AttachmentController : ControllerBase
{
    private readonly IMediator mediator;

    public AttachmentController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet("get-by-id/{id:long}")]
    public async Task<IActionResult> GetByIdAsync(long id)
        => Ok(await this.mediator.Send(new GetAttachmentQuery() { Id = id}));

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllAsync()
        => Ok(await this.mediator.Send(new GetAllAttachmentsQuery()));
}
