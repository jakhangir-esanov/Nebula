using Nebula.Domain.Entities.Payments;
using Nebula.Domain.Enums;

namespace Nebula.Application.Commands.Payments.CreatePaymentHistory;

public record CreatePaymentHistoryCommand : IRequest<PaymentHistory>
{
    public CreatePaymentHistoryCommand(DateTime date, double amount, PaymentType paymentType, long paymentId,
        long customerId, long rentalId)
    {
        Date = date;
        Amount = amount;
        PaymentType = paymentType;
        PaymentId = paymentId;
        CustomerId = customerId;
        RentalId = rentalId;
    }

    public DateTime Date { get; set; }
    public double Amount { get; set; }
    public PaymentType PaymentType { get; set; }
    public long PaymentId { get; set; }
    public long CustomerId { get; set; }
    public long RentalId { get; set; }
}

public class CreatePaymentHistoryCommandHandler : IRequestHandler<CreatePaymentHistoryCommand, PaymentHistory>
{
    private readonly IRepository<PaymentHistory> repository;

    public CreatePaymentHistoryCommandHandler(IRepository<PaymentHistory> repository)
    {
        this.repository = repository;
    }

    public async Task<PaymentHistory> Handle(CreatePaymentHistoryCommand request, CancellationToken cancellationToken)
    {
        var paymentHistory = await repository.SelectAsync(x => x.PaymentId.Equals(request.PaymentId));
        if (paymentHistory is not null)
            throw new AlreadyExistException("Payment History is already exist!");

        var newPaymentHistory = new PaymentHistory()
        {
            Amount = request.Amount,
            PaymentType = request.PaymentType,
            Date = request.Date,
            PaymentId = request.PaymentId,
            CustomerId = request.CustomerId,
            RentalId = request.RentalId
        };

        await repository.InsertAsync(newPaymentHistory);
        await repository.SaveAsync();

        return newPaymentHistory;
    }
}
