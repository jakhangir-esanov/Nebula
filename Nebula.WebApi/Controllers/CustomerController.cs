using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nebula.Application.Commands.People.CreateCustomer;

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
    public async Task<IActionResult> CreateAsync(CreateCustomerCommand dto)
        => Ok(await this.mediator.Send(new CreateCustomerCommand(dto.FirstName, dto.LastName,dto.Email,
            dto.Phone, dto.Password, dto.DateOfBirth, dto.Address, dto.DrivingLicenseNumber, dto.DrivingLicenseExpirationDate)));
}
