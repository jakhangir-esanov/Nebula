using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nebula.Application.Commands.Rentals.CreateCarRental;
using Nebula.Application.Commands.Rentals.DeleteCarRental;
using Nebula.Application.Commands.Rentals.UpdateCarRental;
using Nebula.Application.Queries.Rentals.GetCarRental;

namespace Nebula.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CarRentalController : ControllerBase
{
    private readonly IMediator mediator;

    public CarRentalController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync(CreateCarRentalCommand command)
        => Ok(await this.mediator.Send(new CreateCarRentalCommand(command.CarId, command.RentalId)));

    [HttpPut("update")]
    public async Task<IActionResult> UpdateAsync(UpdateCarRentalCommand command)
        => Ok(await this.mediator.Send(new UpdateCarRentalCommand(command.Id, command.CarId, command.RentalId)));

    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
        => Ok(await this.mediator.Send(new DeleteCarRentalCommand(id)));

    [HttpGet("get-by-id/{id:long}")]
    public async Task<IActionResult> GetAsync(long id)
        => Ok(await this.mediator.Send(new GetCarRentalQuery() { Id = id }));

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllAsync()

        => Ok(await this.mediator.Send(new GetAllCarRentalsQuery()));
}
