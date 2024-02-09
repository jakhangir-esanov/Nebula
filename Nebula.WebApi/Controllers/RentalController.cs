using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Nebula.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RentalController : ControllerBase
{
    private readonly IMediator mediator;

    public RentalController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync(CreateRentalCommand command)
        => Ok(await this.mediator.Send(new CreateRentalCommand(command.CustomerId, command.CarId, command.StartDate, command.EndDate)));

    [HttpPut("update")]
    public async Task<IActionResult> UpdateAsync(UpdateRentalCommand command)
        => Ok(await this.mediator.Send(new UpdateRentalCommand(command.Id,
            command.CustomerId, command.CarId, command.StartDate, command.EndDate)));

    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
        => Ok(await this.mediator.Send(new DeleteRentalCommand(id)));

    [AllowAnonymous]
    [HttpGet("get-by-id/{id:long}")]
    public async Task<IActionResult> GetByIdAsync(long id)
        => Ok(await this.mediator.Send(new GetRentalQuery() { Id = id }));

    [AllowAnonymous]
    [HttpGet("get-all")]
    public async Task<IActionResult> GetAll()
        => Ok(await this.mediator.Send(new GetAllRentalsQuery()));
}
