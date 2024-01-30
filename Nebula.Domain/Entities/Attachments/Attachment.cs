using Nebula.Domain.Commons;
using Nebula.Domain.Entities.Cars;

namespace Nebula.Domain.Entities.Attachments;

public sealed class Attachment : Auditable
{
    public string FileName { get; set; }
    public string FilePath { get; set; }
    
    public long? CarId {  get; set; }
    public Car Car { get; set; }
    public ICollection<OfficeAttachment> OfficeAttachments { get; set; }
}
