namespace Nebula.Application.DTOs;

public class RentalResultDto
{
    public long Id { get; set; }
    public long CustomerId { get; set; }
    public long CarId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
