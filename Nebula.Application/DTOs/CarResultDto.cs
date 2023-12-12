using System.Reflection.Metadata.Ecma335;

namespace Nebula.Application.DTOs;

public class CarResultDto
{
    public long Id { get; set; }
    public string Model { get; set; }
    public DateTime Year { get; set; }
    public string Color { get; set; }
    public string Number { get; set; }
    public long RegistrationNumber { get; set; }
    public bool IsAvailable { get; set; } = true;
    public long CarCategoryId { get; set; }
    public ICollection<CarAttachmentResultDto> CarAttachments { get; set; }
}
