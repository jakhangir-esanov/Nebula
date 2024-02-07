using Nebula.Domain.Commons;
using Nebula.Domain.Entities.Cars;

namespace Nebula.Domain.Entities.Rentals;

public sealed class CarRental : Auditable
{
    public long CarId { get; set; }
    public long RentalId { get; set; }

    public Car Car { get; set; }
    public Rental Rental { get; set; }
}
