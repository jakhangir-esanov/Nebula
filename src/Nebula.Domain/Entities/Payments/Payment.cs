using Nebula.Domain.Commons;
using Nebula.Domain.Entities.People;
using Nebula.Domain.Entities.Rentals;
using Nebula.Domain.Enums;

namespace Nebula.Domain.Entities.Payments;

public sealed class Payment : Auditable
{
    public double Amount { get; set; }
    public PaymentType PaymentType { get; set; }

    public PaymentStatus PaymentStatus { get; set; }

    public long CustomerId { get; set; }
    public Customer Customer { get; set; }

    public long RentalId { get; set; }
    public Rental Rental { get; set; }
}
