using Nebula.Domain.Commons;

namespace Nebula.Domain.Entities.Attachments;

public sealed class Attachment : Auditable
{
    public string FileName { get; set; }
    public string FilePath { get; set; }

    public ICollection<CarAttachment> CarAttachments { get; set; }
    public ICollection<OfficeAttachment> OfficeAttachments { get; set; }
}
