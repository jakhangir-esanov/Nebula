using Nebula.Domain.Commons;
using Nebula.Domain.Entities.Attachments;

namespace Nebula.Domain.Entities.Cars;

public sealed class Car : Auditable
{
    public string Model { get; set; }
    public DateTime Year { get; set; }
    public string Color { get; set; }
    public string Number { get; set; }
    public long RegistrationNumber { get; set; }
    public bool IsAvailable { get; set; } = true;

    public long CarCategoryId { get; set; }
    public CarCategory CarCategory { get; set; }

    public ICollection<CarAttachment> CarAttachments { get; set; }
    public ICollection<Attachment> Attachments { get; set; }
}
