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

    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync(CreateInsuranceCoverageCommand command)
        => Ok(await this.mediator.Send(new CreateInsuranceCoverageCommand(command.Name, command.Description, command.Cost)));

    [HttpPut("update")]
    public async Task<IActionResult> UpdateAsync(UpdateInsuranceCoverageCommand command)
        => Ok(await this.mediator.Send(new UpdateInsuranceCoverageCommand(command.Id, command.Name, command.Description, command.Cost)));

    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
        => Ok(await this.mediator.Send(new DeleteInsuranceCoverageCommand(id)));

    [HttpGet("get-by-id/{id:long}")]
    public async Task<IActionResult> GetByIdAsync(long id)
        => Ok(await this.mediator.Send(new GetInsuranceCoverageQuery() { Id = id }));

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAll()
        => Ok(await this.mediator.Send(new GetAllInsuranceCoveragesQuery()));
}
