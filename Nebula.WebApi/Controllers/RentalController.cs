using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nebula.Application.Commands.Rentals.CreateRental;
using Nebula.Application.Commands.Rentals.DeleteRental;
using Nebula.Application.Commands.Rentals.UpdateRental;
using Nebula.Application.Queries.Rentals.GetRental;

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

    [HttpGet("get-by-id/{id:long}")]
    public async Task<IActionResult> GetByIdAsync(long id)
        => Ok(await this.mediator.Send(new GetRentalQuery() { Id = id}));

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAll()
        => Ok(await this.mediator.Send(new GetAllRentalsQuery()));
}
