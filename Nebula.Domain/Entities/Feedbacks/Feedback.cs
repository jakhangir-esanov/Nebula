using Nebula.Domain.Commons;
using Nebula.Domain.Entities.People;
using Nebula.Domain.Entities.Rentals;
using Nebula.Domain.Enums;

namespace Nebula.Domain.Entities.Feedbacks;

public sealed class Feedback : Auditable
{
    public Rating Rating { get; set; }
    public string Comment { get; set; }
    public DateTime FeedbackDate { get; set; }

    public long RentalId { get; set; }
    public Rental Rental { get; set; }

    public long CustomerId { get; set; }
    public Customer Customer { get; set; }
}
