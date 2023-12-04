using Nebula.Domain.Entities.Payments;
using Nebula.Domain.Entities.People;
using Nebula.Domain.Enums;

namespace Nebula.Application.Commands.Payments.CreatePayment;

public record CreatePaymentCommand : IRequest<Payment>
{
    public CreatePaymentCommand(double amount, PaymentType paymentType, long customerId, long rentalId, 
        PaymentStatus paymentStatus)
    {
        Amount = amount;
        PaymentType = paymentType;
        CustomerId = customerId;
        RentalId = rentalId;
        PaymentStatus = paymentStatus;
    }

    public double Amount { get; set; }
    public PaymentType PaymentType { get; set; }
    public long CustomerId { get; set; }
    public long RentalId { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
}

public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, Payment>
{
    private readonly IRepository<Payment> repository;

    public CreatePaymentCommandHandler(IRepository<Payment> repository)
    {
        this.repository = repository;
    }

    public async Task<Payment> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
    {
        var payment = await repository.SelectAsync(x => x.PaymentStatus.Equals(Domain.Enums.PaymentStatus.success));
        if (payment is not null)
            throw new AlreadyExistException("Payment is already complete!");

        var newPayment = new Payment()
        {
            Amount = request.Amount,
            PaymentType = request.PaymentType,
            CustomerId = request.CustomerId,
            RentalId = request.RentalId,
            PaymentStatus = request.PaymentStatus
        };

        await repository.InsertAsync(newPayment);
        await repository.SaveAsync();

        return newPayment;
    }
}
