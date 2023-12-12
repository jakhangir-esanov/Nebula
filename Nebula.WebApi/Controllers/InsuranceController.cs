using Microsoft.AspNetCore.Authorization;
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

    [Authorize(Roles = "superAdmin")]
    [Authorize(Roles = "admin")]
    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync(CreateInsuranceCommand command)
        => Ok(await this.mediator.Send(new CreateInsuranceCommand(command.Name, command.InsuranceCoverageId, command.CustomerId)));

    [Authorize(Roles = "superAdmin")]
    [Authorize(Roles = "admin")]
    [HttpPut("update")]
    public async Task<IActionResult> UpdateAsync(UpdateInsuranceCommand command)
        => Ok(await this.mediator.Send(new UpdateInsuranceCommand(command.Id, command.Name, command.InsuranceCoverageId, command.CustomerId)));

    [Authorize(Roles = "superAdmin")]
    [Authorize(Roles = "admin")]
    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
        => Ok(await this.mediator.Send(new DeleteInsuranceCommand(id)));

    [AllowAnonymous]
    [HttpGet("get-by-id/{id:long}")]
    public async Task<IActionResult> GetByIdAsync(long id)
        => Ok(await this.mediator.Send(new GetInsuranceQuery() { Id = id }));
    
    [AllowAnonymous]
    [HttpGet("get-all")]
    public async Task<IActionResult> GetAll()
        => Ok(await this.mediator.Send(new GetAllInsurancesQuery()));


}
