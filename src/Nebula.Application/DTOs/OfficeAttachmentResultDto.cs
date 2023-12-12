using Nebula.Domain.Entities.Offices;

namespace Nebula.Application.DTOs;

public class OfficeAttachmentResultDto
{
    public long Id { get; set; }
    public long OfficeId { get; set; }
    public long AttachmentId { get; set; }
}
