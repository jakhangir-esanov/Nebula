using Nebula.Domain.Commons;
using Nebula.Domain.Entities.Attachments;
using Nebula.Domain.Entities.Payments;

namespace Nebula.Domain.Entities.People;

public sealed class Customer : Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Password { get; set; }
    public string Salt { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Address { get; set; }
    public string DrivingLicenseNumber { get; set; }
    public DateTime DrivingLicenseExpirationDate { get; set; }

    public long? AttachmentId { get; set; }
    public Attachment Attachment { get; set; }

    public ICollection<PaymentHistory> PaymentHistories { get; set; }
}
