using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nebula.Application.Commands.People.CreateUser;
using Nebula.Application.Queries.People.GetUser;

namespace Nebula.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IMediator mediator;

    public UserController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync(CreateUserCommand dto)
        => Ok(await this.mediator.Send(new CreateUserCommand(dto.FirstName, dto.LastName, dto.username, dto.Email,
            dto.Phone, dto.Password, dto.DateOfBirth, dto.UserRole, dto.OfficeId)));

    [HttpGet("get-by-id/{id:long}")]
    public async Task<IActionResult> GetbyIdAsync(long id)
        => Ok(await this.mediator.Send(new RetrieveByIdQuery() { Id = id }));
}
