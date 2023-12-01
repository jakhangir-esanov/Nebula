using Nebula.Domain.Commons;
using Nebula.Domain.Entities.Cars;

namespace Nebula.Domain.Entities.Attachments;

public sealed class CarAttachment : Auditable
{
    public long CarId { get; set; }
    public Car Car { get; set; }

    public long AttamentId { get; set; }
    public Attachment Attachment { get; set; }
}
