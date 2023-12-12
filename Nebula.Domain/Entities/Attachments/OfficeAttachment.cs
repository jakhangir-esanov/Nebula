using Nebula.Domain.Commons;
using Nebula.Domain.Entities.Offices;

namespace Nebula.Domain.Entities.Attachments;

public sealed class OfficeAttachment : Auditable
{
    public long OfficeId { get; set; }
    public Office Office { get; set; }

    public long AttachmentId { get; set; }
    public Attachment Attachment { get; set; }
}
