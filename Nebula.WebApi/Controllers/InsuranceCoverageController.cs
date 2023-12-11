using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Nebula.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InsuranceCoverageController : ControllerBase
{
    private readonly IMediator mediator;

    public InsuranceCoverageController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [Authorize(Roles = "superAdmin")]
    [Authorize(Roles = "admin")]
    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync(CreateInsuranceCoverageCommand command)
        => Ok(await this.mediator.Send(new CreateInsuranceCoverageCommand(command.Name, command.Description, command.Cost)));
    
    [Authorize(Roles = "superAdmin")]
    [Authorize(Roles = "admin")]
    [HttpPut("update")]
    public async Task<IActionResult> UpdateAsync(UpdateInsuranceCoverageCommand command)
        => Ok(await this.mediator.Send(new UpdateInsuranceCoverageCommand(command.Id, command.Name, command.Description, command.Cost)));

    [Authorize(Roles = "superAdmin")]
    [Authorize(Roles = "admin")]
    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
        => Ok(await this.mediator.Send(new DeleteInsuranceCoverageCommand(id)));

    [AllowAnonymous]
    [HttpGet("get-by-id/{id:long}")]
    public async Task<IActionResult> GetByIdAsync(long id)
        => Ok(await this.mediator.Send(new GetInsuranceCoverageQuery() { Id = id }));

    [AllowAnonymous]
    [HttpGet("get-all")]
    public async Task<IActionResult> GetAll()
        => Ok(await this.mediator.Send(new GetAllInsuranceCoveragesQuery()));
}
