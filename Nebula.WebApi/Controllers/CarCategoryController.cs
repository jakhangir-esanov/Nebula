using Microsoft.AspNetCore.Mvc;

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

    [HttpPut("update")]
    public async Task<IActionResult> UpdateAsync(UpdateCarCategoryCommand command)
        => Ok(await this.mediator.Send(new UpdateCarCategoryCommand(command.Id, command.Name, command.Price, command.Description, command.Discount)));

    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
        => Ok(await this.mediator.Send(new DeleteCarCategoryCommand(id)));

    [HttpGet("get-by-id/{id:long}")]
    public async Task<IActionResult> GetByIdAsync(long id)
        => Ok(await this.mediator.Send(new GetCarCategoryQuery() { Id = id }));

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAll()
        => Ok(await this.mediator.Send(new GetAllCarCategoriesQuery()));
}
