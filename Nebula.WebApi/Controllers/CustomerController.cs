using Microsoft.AspNetCore.Authorization;

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
        => Ok(await this.mediator.Send(new CreateCustomerCommand(command.FirstName, command.LastName, command.Email,
            command.Phone, command.Password, command.DateOfBirth, command.Address, command.DrivingLicenseNumber,
            command.DrivingLicenseExpirationDate)));

    [HttpPost("upload-image")]
    public async Task<IActionResult> UploadImageAsync(long customerId, [FromForm] AttachmentCreationDto dto)
        => Ok(await this.mediator.Send(new UploadCustomerImageCommand(customerId, dto)));

    [HttpPut("update")]
    public async Task<IActionResult> UpdateAsync(UpdateCustomerCommand command)
        => Ok(await this.mediator.Send(new UpdateCustomerCommand(command.Id, command.FirstName, command.LastName, command.Email,
            command.Phone, command.Password, command.DateOfBirth, command.Address, command.DrivingLicenseNumber,
            command.DrivingLicenseExpirationDate)));

    [HttpPut("update-image")]
    public async Task<IActionResult> UpdateImageAsync(long customerId, [FromForm] AttachmentCreationDto dto)
        => Ok(await this.mediator.Send(new UpdateCustomerImageCommand(customerId, dto)));

    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
        => Ok(await this.mediator.Send(new DeleteCustomerCommand(id)));


    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteImageAsync(long id)
        => Ok(await this.mediator.Send(new DeleteCustomerImageCommand(id)));

    [AllowAnonymous]
    [HttpGet("get-by-id/{id:long}")]
    public async Task<IActionResult> GetByIdAsync(long id)
        => Ok(await this.mediator.Send(new GetCustomerQuery() { Id = id }));

    [AllowAnonymous]
    [HttpGet("get-all")]
    public async Task<IActionResult> GetAll()
        => Ok(await this.mediator.Send(new GetAllCustomersQuery()));
}
