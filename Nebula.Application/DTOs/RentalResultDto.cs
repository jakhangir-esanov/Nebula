namespace Nebula.Application.DTOs;

public class RentalResultDto
{
    public RentalResultDto()
    {
        CarRentals = new HashSet<CarRentalResultDto>();
    }
    public long Id { get; set; }
    public long CustomerId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public ICollection<CarRentalResultDto> CarRentals { get; set; }
}
