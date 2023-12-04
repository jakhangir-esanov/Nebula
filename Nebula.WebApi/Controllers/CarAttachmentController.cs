using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nebula.Application.Queries.Attachments.GetCarAttachment;

namespace Nebula.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CarAttachmentController : ControllerBase
{
    private readonly IMediator mediator;

    public CarAttachmentController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAll()
        => Ok(await this.mediator.Send(new GetAllCarAttachmentQuery()));
}
