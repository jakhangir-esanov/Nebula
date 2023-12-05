using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nebula.Application.Commands.Payments.CreatePayment;
using Nebula.Application.Commands.Payments.DeletePayment;
using Nebula.Application.Commands.Payments.UpdatePayment;
using Nebula.Application.Queries.Payments.GetPayment;
using Npgsql.Replication;
using System.Runtime.CompilerServices;

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

    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync(CreatePaymentCommand command)
        => Ok(await this.mediator.Send(new CreatePaymentCommand(command.Amount, command.PaymentType, 
            command.CustomerId, command.RentalId, command.PaymentStatus)));

    [HttpPut("update")]
    public async Task<IActionResult> UpdateAsync(UpdatePaymentCommand command)
        => Ok(await this.mediator.Send(new UpdatePaymentCommand(command.Id, command.Amount, command.PaymentType,
            command.CustomerId, command.RentalId, command.PaymentStatus)));

    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
        => Ok(await this.mediator.Send(new DeletePaymentCommand(id)));

    [HttpGet("get-by-id/{id:long}")]
    public async Task<IActionResult> GetByIdAsync(long id)
        => Ok(await this.mediator.Send(new GetPaymentQuery() { Id = id }));

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAll()
        => Ok(await this.mediator.Send(new GetAllPaymentsQuery()));
}
