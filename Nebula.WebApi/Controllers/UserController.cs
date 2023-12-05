using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nebula.Application.Commands.People.CreateUser;
using Nebula.Application.Commands.People.DeleteUser;
using Nebula.Application.Commands.People.UpdateUser;
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
    public async Task<IActionResult> CreateAsync(CreateUserCommand command)
        => Ok(await this.mediator.Send(new CreateUserCommand(command.FirstName, command.LastName, command.username, command.Email,
            command.Phone, command.Password, command.DateOfBirth, command.UserRole, command.OfficeId)));

    [HttpPut("update")]
    public async Task<IActionResult> UpdateAsync(UpdateUserCommand command)
        => Ok(await this.mediator.Send(new UpdateUserCommand(command.Id, command.FirstName, command.LastName, command.username, command.Email,
            command.Phone, command.Password, command.DateOfBirth, command.UserRole, command.OfficeId)));

    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
        => Ok(await this.mediator.Send(new DeleteUserCommand(id)));

    [HttpGet("get-by-id/{id:long}")]
    public async Task<IActionResult> GetByIdAsync(long id)
        => Ok(await this.mediator.Send(new GetUserQuery() { Id = id }));

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAll()
        => Ok(await this.mediator.Send(new GetAllUsersQuery()));
}
