using Microsoft.AspNetCore.Authorization;

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

    [HttpPost("upload-image")]
    public async Task<IActionResult> UploadImageAsync(long id, [FromForm] AttachmentCreationDto dto)
        => Ok(await this.mediator.Send(new UploadCarImageCommand(id, dto)));

    [HttpPut("update")]
    public async Task<IActionResult> UpdateAsync(UpdateCarCommand command)
        => Ok(await this.mediator.Send(new UpdateCarCommand(command.Id, command.Model, command.Year, command.Color,
            command.Number, command.RegistrationNumber, command.IsAvailable, command.CarCategoryId)));

    [HttpPut("update-car-image")]
    public async Task<IActionResult> UpdateCarImageAsync(long carId, long attachmentId, [FromForm] AttachmentCreationDto dto)
        => Ok(await this.mediator.Send(new UpdateCarImageCommand(carId, attachmentId, dto)));

    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
        => Ok(await this.mediator.Send(new DeleteCarCommand(id)));

    [HttpDelete("delete-car-attachment")]
    public async Task<IActionResult> DeleteCarAttachmentAsync(long carId, long attachmentId)
        => Ok(await this.mediator.Send(new DeleteCarImageСommand(carId, attachmentId)));

    [HttpGet("get-by-id/{id:long}")]
    public async Task<IActionResult> GetByIdAsync(long id)
        => Ok(await this.mediator.Send(new GetCarQuery() { Id = id }));

    [AllowAnonymous]
    [HttpGet("get-all-attachments/{id:long}")]
    public async Task<IActionResult> GetAllAttachments(long id)
        => Ok(await this.mediator.Send(new GetAllCarAttachmentsQuery() { Id = id }));

    [AllowAnonymous]
    [HttpGet("get-all")]
    public async Task<IActionResult> GetAll()
        => Ok(await this.mediator.Send(new GetAllCarsQuery()));
}
