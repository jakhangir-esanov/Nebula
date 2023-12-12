using Nebula.Domain.Enums;

namespace Nebula.Application.DTOs;

public class PaymentResultDto
{
    public long Id { get; set; }
    public double Amount { get; set; }
    public PaymentType PaymentType { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
    public long CustomerId { get; set; }
    public long RentalId { get; set; }
}
