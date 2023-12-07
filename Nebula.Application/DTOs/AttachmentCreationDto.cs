using Microsoft.AspNetCore.Http;

namespace Nebula.Application.DTOs;

public class AttachmentCreationDto
{
    public IFormFile FormFile { get; set; }
}
