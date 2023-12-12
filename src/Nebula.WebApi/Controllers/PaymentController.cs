using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Nebula.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentController : ControllerBase
{
    private readonly IMediator mediator;

    public PaymentController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [Authorize(Roles = "superAdmin")]
    [Authorize(Roles = "admin")]
    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync(CreatePaymentCommand command)
        => Ok(await this.mediator.Send(new CreatePaymentCommand(command.Amount, command.PaymentType,
            command.CustomerId, command.RentalId, command.PaymentStatus)));

    [Authorize(Roles = "superAdmin")]
    [Authorize(Roles = "admin")]
    [HttpPut("update")]
    public async Task<IActionResult> UpdateAsync(UpdatePaymentCommand command)
        => Ok(await this.mediator.Send(new UpdatePaymentCommand(command.Id, command.Amount, command.PaymentType,
            command.CustomerId, command.RentalId, command.PaymentStatus)));

    [Authorize(Roles = "superAdmin")]
    [Authorize(Roles = "admin")]
    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
        => Ok(await this.mediator.Send(new DeletePaymentCommand(id)));

    [AllowAnonymous]
    [HttpGet("get-by-id/{id:long}")]
    public async Task<IActionResult> GetByIdAsync(long id)
        => Ok(await this.mediator.Send(new GetPaymentQuery() { Id = id }));

    [AllowAnonymous]
    [HttpGet("get-all")]
    public async Task<IActionResult> GetAll()
        => Ok(await this.mediator.Send(new GetAllPaymentsQuery()));
}
