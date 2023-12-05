using Microsoft.AspNetCore.Mvc;

namespace Nebula.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OfficeController : ControllerBase
{
    private readonly IMediator mediator;

    public OfficeController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync(CreateOfficeCommand command)
        => Ok(await this.mediator.Send(new CreateOfficeCommand(command.Name, command.Address, command.City, command.State,
            command.PostalCode, command.Country, command.Phone, command.Email, command.Website, command.Website)));

    [HttpPut("update")]
    public async Task<IActionResult> UpdateAsync(UpdateOfficeCommand command)
        => Ok(await this.mediator.Send(new UpdateOfficeCommand(command.Id, command.Name, command.Address, command.City,
            command.State, command.PostalCode, command.Country, command.Phone, command.Email, command.Website, command.Website)));

    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
        => Ok(await this.mediator.Send(new DeleteOfficeCommand(id)));

    [HttpGet("get-by-id/{id:long}")]
    public async Task<IActionResult> GetByIdAsync(long id)
        => Ok(await this.mediator.Send(new GetOfficeQuery() { Id = id }));

    [HttpGet("get-all-attachments/{id:long}")]
    public async Task<IActionResult> GetAllAttachments(long id)
        => Ok(await this.mediator.Send(new GetAllOfficeAttachmentsQuery() { Id = id }));

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAll()
        => Ok(await this.mediator.Send(new GetAllOfficesQuery()));
}
