using Microsoft.Extensions.DependencyInjection;
using Nebula.Domain.Entities.Payments;
using Nebula.Domain.Enums;
using System.Text.Json;

namespace Nebula.Application.Commands.Payments.UpdatePaymentHistory;

public record UpdatePaymentHistoryCommand : IRequest<PaymentHistory>
{
    public UpdatePaymentHistoryCommand(long id, DateTime date, double amount, PaymentType paymentType, 
        long paymentId, long customerId, long rentalId)
    {
        Id = id;
        Date = date;
        Amount = amount;
        PaymentType = paymentType;
        PaymentId = paymentId;
        CustomerId = customerId;
        RentalId = rentalId;
    }

    public long Id { get; set; }
    public DateTime Date { get; set; }
    public double Amount { get; set; }
    public PaymentType PaymentType { get; set; }
    public long PaymentId { get; set; }
    public long CustomerId { get; set; }
    public long RentalId { get; set; }
}

public class UpdatePaymentHistoryCommandHandler : IRequestHandler<UpdatePaymentHistoryCommand, PaymentHistory>
{
    private readonly IRepository<PaymentHistory> repository;

    public UpdatePaymentHistoryCommandHandler(IRepository<PaymentHistory> repository)
    {
        this.repository = repository;
    }

    public async Task<PaymentHistory> Handle(UpdatePaymentHistoryCommand request, CancellationToken cancellationToken)
    {
        var paymentHistory = await repository.SelectAsync(x => x.Id.Equals(request.Id))
            ?? throw new NotFoundException("Payment History was not found!");

        paymentHistory.Date = request.Date;
        paymentHistory.Amount = request.Amount;
        paymentHistory.PaymentType = request.PaymentType;
        paymentHistory.PaymentId = request.PaymentId;
        paymentHistory.CustomerId = request.CustomerId;
        paymentHistory.RentalId = request.RentalId;

        repository.Update(paymentHistory);
        await repository.SaveAsync();

        return paymentHistory;
    }
}
