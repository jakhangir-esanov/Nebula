using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

    [Authorize(Roles = "superAdmin")]
    [Authorize(Roles = "admin")]

    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync(CreateUserCommand command)
        => Ok(await this.mediator.Send(new CreateUserCommand(command.FirstName, command.LastName, command.username, command.Email,
            command.Phone, command.Password, command.DateOfBirth, command.UserRole, command.OfficeId)));

    [Authorize(Roles = "superAdmin")]
    [Authorize(Roles = "admin")]

    [HttpPost("upload-image")]
    public async Task<IActionResult> UploadImageAsync(long userId, [FromForm] AttachmentCreationDto dto)
        => Ok(await this.mediator.Send(new UploadUserImageCommand(userId, dto)));

    [Authorize(Roles = "superAdmin")]
    [Authorize(Roles = "admin")]

    [HttpPut("update")]
    public async Task<IActionResult> UpdateAsync(UpdateUserCommand command)
        => Ok(await this.mediator.Send(new UpdateUserCommand(command.Id, command.FirstName, command.LastName, command.username, command.Email,
            command.Phone, command.Password, command.DateOfBirth, command.UserRole, command.OfficeId)));

    [Authorize(Roles = "superAdmin")]
    [Authorize(Roles = "admin")]

    [HttpPut("update-image")]
    public async Task<IActionResult> UpdateImageAsync(long userId, [FromForm] AttachmentCreationDto dto)
        => Ok(await this.mediator.Send(new UpdateUserImageCommand(userId, dto)));

    [Authorize(Roles = "superAdmin")]
    [Authorize(Roles = "admin")]

    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
        => Ok(await this.mediator.Send(new DeleteUserCommand(id)));

    [Authorize(Roles = "superAdmin")]
    [Authorize(Roles = "admin")]

    [HttpDelete("delete-image")]
    public async Task<IActionResult> DeleteImageAsync(long userId)
        => Ok(await this.mediator.Send(new DeleteUserImageCommand(userId)));

    [Authorize(Roles = "superAdmin")]
    [Authorize(Roles = "admin")]

    [HttpGet("get-by-id/{id:long}")]
    public async Task<IActionResult> GetByIdAsync(long id)
        => Ok(await this.mediator.Send(new GetUserQuery() { Id = id }));

    [Authorize(Roles = "superAdmin")]
    [Authorize(Roles = "admin")]

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAll()
        => Ok(await this.mediator.Send(new GetAllUsersQuery()));
}
