using Microsoft.AspNetCore.Authorization;

namespace Nebula.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CarCategoryController : ControllerBase
{
    private readonly IMediator mediator;

    public CarCategoryController(IMediator mediator)
    {
        this.mediator = mediator;
    }


    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync(CreateCarCategoryCommand command)
        => Ok(await this.mediator.Send(new CreateCarCategoryCommand(command.Name, command.Price, command.Description, command.Discount)));

    [HttpPost("upload-image")]
    public async Task<IActionResult> UploadImageAsync(long carCategoryId, [FromForm] AttachmentCreationDto dto)
        => Ok(await this.mediator.Send(new UploadCarCategoryImageCommand(carCategoryId, dto)));

    [HttpPut("update")]
    public async Task<IActionResult> UpdateAsync(UpdateCarCategoryCommand command)
        => Ok(await this.mediator.Send(new UpdateCarCategoryCommand(command.Id, command.Name, command.Price, command.Description, command.Discount)));

    [HttpPut("update-image")]
    public async Task<IActionResult> UpdateImageAsync(long carCategoryId, [FromForm] AttachmentCreationDto dto)
        => Ok(await this.mediator.Send(new UpdateCarCategoryImageCommand(carCategoryId, dto)));

    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
        => Ok(await this.mediator.Send(new DeleteCarCategoryCommand(id)));

    [HttpDelete("delete-image")]
    public async Task<IActionResult> DeleteImageAsync(long id)
        => Ok(await this.mediator.Send(new DeleteCarCategoryImageCommand(id)));

    [AllowAnonymous]
    [HttpGet("get-by-id/{id:long}")]
    public async Task<IActionResult> GetByIdAsync(long id)
        => Ok(await this.mediator.Send(new GetCarCategoryQuery() { Id = id }));

    [AllowAnonymous]
    [HttpGet("get-all")]
    public async Task<IActionResult> GetAll()
        => Ok(await this.mediator.Send(new GetAllCarCategoriesQuery()));
}
