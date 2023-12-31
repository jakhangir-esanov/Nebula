﻿using Microsoft.AspNetCore.Authorization;

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

    [AllowAnonymous]
    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync(CreateOfficeCommand command)
        => Ok(await this.mediator.Send(new CreateOfficeCommand(command.Name, command.Address, command.City, command.State,
            command.PostalCode, command.Country, command.Phone, command.Email, command.Website, command.Website)));

    [AllowAnonymous]
    [HttpPost("upload-image")]
    public async Task<IActionResult> UploadImageAsync(long officeId, [FromForm] AttachmentCreationDto dto)
        => Ok(await this.mediator.Send(new UploadOfficeImageCommand(officeId, dto)));

    [AllowAnonymous]
    [HttpPut("update")]
    public async Task<IActionResult> UpdateAsync(UpdateOfficeCommand command)
        => Ok(await this.mediator.Send(new UpdateOfficeCommand(command.Id, command.Name, command.Address, command.City,
            command.State, command.PostalCode, command.Country, command.Phone, command.Email, command.Website, command.Website)));

    [AllowAnonymous]
    [HttpPut("update-image")]
    public async Task<IActionResult> UpdateImageAsync(long officeId, long attachmentId, [FromForm] AttachmentCreationDto dto)
        => Ok(await this.mediator.Send(new UpdateOfficeImageCommnd(officeId, attachmentId, dto)));

    [AllowAnonymous]
    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
        => Ok(await this.mediator.Send(new DeleteOfficeCommand(id)));

    [AllowAnonymous]
    [HttpDelete("delete-image")]
    public async Task<IActionResult> DeleteImageAsync(long officeId, long attachmentId)
        => Ok(await this.mediator.Send(new DeleteOfficeImageCommand(officeId, attachmentId)));

    [AllowAnonymous]
    [HttpGet("get-by-id/{id:long}")]
    public async Task<IActionResult> GetByIdAsync(long id)
        => Ok(await this.mediator.Send(new GetOfficeQuery() { Id = id }));

    [AllowAnonymous]
    [HttpGet("get-all-attachments/{id:long}")]
    public async Task<IActionResult> GetAllAttachments(long id)
        => Ok(await this.mediator.Send(new GetAllOfficeAttachmentsQuery() { Id = id }));

    [AllowAnonymous]
    [HttpGet("get-all")]
    public async Task<IActionResult> GetAll()
        => Ok(await this.mediator.Send(new GetAllOfficesQuery()));
}
