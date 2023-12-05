using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nebula.Application.Commands.People.CreateCustomer;
using Nebula.Application.Commands.People.DeleteCustomer;
using Nebula.Application.Commands.People.UpdateCustomer;
using Nebula.Application.Queries.People.GetCustomer;

namespace Nebula.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly IMediator mediator;

    public CustomerController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync(CreateCustomerCommand command)
        => Ok(await this.mediator.Send(new CreateCustomerCommand(command.FirstName, command.LastName,command.Email,
            command.Phone, command.Password, command.DateOfBirth, command.Address, command.DrivingLicenseNumber, 
            command.DrivingLicenseExpirationDate)));

    [HttpPut("update")]
    public async Task<IActionResult> UpdateAsync(UpdateCustomerCommand command)
        => Ok(await this.mediator.Send(new UpdateCustomerCommand(command.Id, command.FirstName, command.LastName, command.Email,
            command.Phone, command.Password, command.DateOfBirth, command.Address, command.DrivingLicenseNumber,
            command.DrivingLicenseExpirationDate)));

    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
        => Ok(await this.mediator.Send(new DeleteCustomerCommand(id)));

    [HttpGet("get-by-id/{id:long}")]
    public async Task<IActionResult> GetByIdAsync(long id)
        => Ok(await this.mediator.Send(new GetCustomerQuery() { Id = id}));

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAll()
        => Ok(await this.mediator.Send(new GetAllCustomersQuery()));
}
