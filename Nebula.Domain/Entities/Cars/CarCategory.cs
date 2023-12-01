using Nebula.Domain.Commons;
using Nebula.Domain.Entities.Attachments;

namespace Nebula.Domain.Entities.Cars;

public sealed class CarCategory : Auditable
{
    public string Name { get; set; }
    public double Price { get; set; }
    public string Description { get; set; }
    public double? Discount { get; set; }

    public ICollection<Car> Cars { get; set; }

    public long? AttachmentId { get; set; }
    public Attachment Attachment { get; set; }
}

