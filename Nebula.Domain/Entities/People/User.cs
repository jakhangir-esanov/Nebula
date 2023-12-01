using Nebula.Domain.Commons;
using Nebula.Domain.Entities.Attachments;
using Nebula.Domain.Entities.Offices;
using Nebula.Domain.Enums;

namespace Nebula.Domain.Entities.People;

public sealed class User : Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string username { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Password { get; set; }
    public DateTime DateOfBirth { get; set; }
    public UserRole UserRole { get; set; }

    public long OfficeId { get; set; }
    public Office Office { get; set; }

    public long? AttachmentId { get; set; }
    public Attachment Attachment { get; set; }
}
