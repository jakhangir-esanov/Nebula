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

    [AllowAnonymous]
    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync(CreateUserCommand command)
        => Ok(await this.mediator.Send(new CreateUserCommand(command.FirstName, command.LastName, command.username, command.Email,
            command.Phone, command.Password, command.DateOfBirth, command.UserRole, command.OfficeId)));

    [AllowAnonymous]
    [HttpPost("upload-image")]
    public async Task<IActionResult> UploadImageAsync(long userId, [FromForm] AttachmentCreationDto dto)
        => Ok(await this.mediator.Send(new UploadUserImageCommand(userId, dto)));

   
    [AllowAnonymous]
    [HttpPut("update")]
    public async Task<IActionResult> UpdateAsync(UpdateUserCommand command)
        => Ok(await this.mediator.Send(new UpdateUserCommand(command.Id, command.FirstName, command.LastName, command.username, command.Email,
            command.Phone, command.Password, command.DateOfBirth, command.UserRole, command.OfficeId)));

    
    [AllowAnonymous]
    [HttpPut("update-image")]
    public async Task<IActionResult> UpdateImageAsync(long userId, [FromForm] AttachmentCreationDto dto)
        => Ok(await this.mediator.Send(new UpdateUserImageCommand(userId, dto)));

    
    [AllowAnonymous]
    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
        => Ok(await this.mediator.Send(new DeleteUserCommand(id)));

   
    [AllowAnonymous]
    [HttpDelete("delete-image")]
    public async Task<IActionResult> DeleteImageAsync(long userId)
        => Ok(await this.mediator.Send(new DeleteUserImageCommand(userId)));

    [AllowAnonymous]
    [HttpGet("get-by-id/{id:long}")]
    public async Task<IActionResult> GetByIdAsync(long id)
        => Ok(await this.mediator.Send(new GetUserQuery() { Id = id }));

    [AllowAnonymous]
    [HttpGet("get-all")]
    public async Task<IActionResult> GetAll()
        => Ok(await this.mediator.Send(new GetAllUsersQuery()));
}
