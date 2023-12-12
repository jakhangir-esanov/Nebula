using Nebula.Domain.Enums;

namespace Nebula.Application.DTOs;

public class PaymentHistoryResultDto
{
    public long Id { get; set; }
    public DateTime Date { get; set; }
    public double Amount { get; set; }
    public PaymentType PaymentType { get; set; }
    public long PaymentId { get; set; }
    public long CustomerId { get; set; }
    public long RentalId { get; set; }
}

