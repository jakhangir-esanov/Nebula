using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Nebula.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentHistoryController : ControllerBase
{
    private readonly IMediator mediator;

    public PaymentHistoryController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [Authorize(Roles = "superAdmin")]
    [Authorize(Roles = "admin")]
    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync(CreatePaymentHistoryCommand command)
        => Ok(await this.mediator.Send(new CreatePaymentHistoryCommand(command.Date, command.Amount,
            command.PaymentType, command.PaymentId, command.CustomerId, command.RentalId)));

    [Authorize(Roles = "superAdmin")]
    [Authorize(Roles = "admin")]
    [HttpPut("update")]
    public async Task<IActionResult> UpdateAsync(UpdatePaymentHistoryCommand command)
        => Ok(await this.mediator.Send(new UpdatePaymentHistoryCommand(command.Id, command.Date, command.Amount,
            command.PaymentType, command.PaymentId, command.CustomerId, command.RentalId)));

    [Authorize(Roles = "superAdmin")]
    [Authorize(Roles = "admin")]
    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
        => Ok(await this.mediator.Send(new DeletePaymentHistoryCommand(id)));

    [AllowAnonymous]
    [HttpGet("get-by-id/{id:long}")]
    public async Task<IActionResult> GetByIdAsync(long id)
        => Ok(await this.mediator.Send(new GetPaymentHistoryQuery() { Id = id }));

    [AllowAnonymous]
    [HttpGet("get-all")]
    public async Task<IActionResult> GetAll()
        => Ok(await this.mediator.Send(new GetAllPaymentHistoriesQuery()));
}
