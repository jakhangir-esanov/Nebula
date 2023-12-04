using Nebula.Domain.Entities.Rentals;
using Nebula.Domain.Enums;

namespace Nebula.Application.DTOs;

public class FeedbackResultDto
{
    public long Id { get; set; }
    public Rating Rating { get; set; }
    public string Comment { get; set; }
    public DateTime FeedbackDate { get; set; }
    public long RentalId { get; set; }
    public long CustomerId { get; set; }
}
