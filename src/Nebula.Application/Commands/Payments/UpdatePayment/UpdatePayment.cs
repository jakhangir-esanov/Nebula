using Nebula.Domain.Entities.Payments;
using Nebula.Domain.Enums;

namespace Nebula.Application.Commands.Payments.UpdatePayment;

public record UpdatePaymentCommand : IRequest<Payment>
{
    public UpdatePaymentCommand(long id, double amount, PaymentType paymentType, long customerId, 
        long rentalId, PaymentStatus paymentStatus)
    {
        Id = id;
        Amount = amount;
        PaymentType = paymentType;
        CustomerId = customerId;
        RentalId = rentalId;
        PaymentStatus = paymentStatus;
    }

    public long Id { get; set; }
    public double Amount { get; set; }
    public PaymentType PaymentType { get; set; }
    public long CustomerId { get; set; }
    public long RentalId { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
}

public class UpdatePaymentCommandHandler : IRequestHandler<UpdatePaymentCommand, Payment>
{
    private readonly IRepository<Payment> repository;

    public UpdatePaymentCommandHandler(IRepository<Payment> repository)
    {
        this.repository = repository;
    }

    public async Task<Payment> Handle(UpdatePaymentCommand request, CancellationToken cancellationToken)
    {
        var payment = await repository.SelectAsync(x => x.Id.Equals(request.Id))
            ?? throw new NotFoundException("Payment was not found!");

        payment.Amount = request.Amount;
        payment.PaymentType = request.PaymentType;
        payment.CustomerId = request.CustomerId;
        payment.RentalId = request.RentalId;
        payment.PaymentStatus = request.PaymentStatus;

        repository.Update(payment);
        await repository.SaveAsync();

        return payment;
    }
}

