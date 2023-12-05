using Microsoft.AspNetCore.Mvc;

namespace Nebula.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CarController : ControllerBase
{
    private readonly IMediator mediator;

    public CarController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync(CreateCarCommand command)
        => Ok(await this.mediator.Send(new CreateCarCommand(command.Model, command.Year, command.Color,
            command.Number, command.RegistrationNumber, command.IsAvailable, command.CarCategoryId)));

    [HttpPut("update")]
    public async Task<IActionResult> UpdateAsync(UpdateCarCommand command)
        => Ok(await this.mediator.Send(new UpdateCarCommand(command.Id, command.Model, command.Year, command.Color,
            command.Number, command.RegistrationNumber, command.IsAvailable, command.CarCategoryId)));

    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
        => Ok(await this.mediator.Send(new DeleteCarCommand(id)));

    [HttpGet("get-by-id/{id:long}")]
    public async Task<IActionResult> GetByIdAsync(long id)
        => Ok(await this.mediator.Send(new GetCarQuery() { Id = id }));

    [HttpGet("get-all-attachments/{id:long}")]
    public async Task<IActionResult> GetAllAttachments(long id)
        => Ok(await this.mediator.Send(new GetAllCarAttachmentsQuery() { Id = id }));

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAll()
        => Ok(await this.mediator.Send(new GetAllCarsQuery()));
}
