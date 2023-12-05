using Microsoft.AspNetCore.Mvc;

namespace Nebula.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InsuranceController : ControllerBase
{
    private readonly IMediator mediator;

    public InsuranceController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync(CreateInsuranceCommand command)
        => Ok(await this.mediator.Send(new CreateInsuranceCommand(command.Name, command.InsuranceCoverageId, command.CustomerId)));

    [HttpPut("update")]
    public async Task<IActionResult> UpdateAsync(UpdateInsuranceCommand command)
        => Ok(await this.mediator.Send(new UpdateInsuranceCommand(command.Id, command.Name, command.InsuranceCoverageId, command.CustomerId)));

    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
        => Ok(await this.mediator.Send(new DeleteInsuranceCommand(id)));

    [HttpGet("get-by-id/{id:long}")]
    public async Task<IActionResult> GetByIdAsync(long id)
        => Ok(await this.mediator.Send(new GetInsuranceQuery() { Id = id }));

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAll()
        => Ok(await this.mediator.Send(new GetAllInsurancesQuery()));


}
