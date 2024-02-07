using Nebula.Domain.Commons;
using Nebula.Domain.Entities.Cars;
using Nebula.Domain.Entities.People;

namespace Nebula.Domain.Entities.Rentals;

public sealed class Rental : Auditable
{
    public long CustomerId { get; set; }
    public Customer Customer { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public ICollection<CarRental> CarRentals { get; set; }
}
