using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nebula.Application.Commands.Offices.CreateOffice;

namespace Nebula.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OfficeController : ControllerBase
{
    private readonly IMediator mediator;

    public OfficeController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync(CreateOfficeCommand dto)
        => Ok(await this.mediator.Send(new CreateOfficeCommand(dto.Name, dto.Address, dto.City, dto.State, dto.PostalCode,
            dto.Country, dto.Phone, dto.Email, dto.Website, dto.Website)));
}
