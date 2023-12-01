using Nebula.Domain.Commons;
using Nebula.Domain.Entities.Attachments;
using Nebula.Domain.Entities.People;

namespace Nebula.Domain.Entities.Offices;

public sealed class Office : Auditable
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Website { get; set; }
    public string Description { get; set; }

    public ICollection<User> Users { get; set; }
    
    public ICollection<OfficeAttachment> OfficeAttachments { get; set; }
    public ICollection<Attachment> Attachments { get; set; }    
}
