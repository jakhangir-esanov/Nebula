using Nebula.Domain.Entities.Payments;

namespace Nebula.Application.Commands.Payments.DeletePaymentHistory;

public record DeletePaymentHistoryCommand : IRequest<bool>
{
    public DeletePaymentHistoryCommand(long id)
    {
        Id = id;
    }

    public long Id { get; set; }
}

public class DeletePaymentHistoryCommandHandler : IRequestHandler<DeletePaymentHistoryCommand, bool>
{
    private readonly IRepository<PaymentHistory> repository;

    public DeletePaymentHistoryCommandHandler(IRepository<PaymentHistory> repository)
    {
        this.repository = repository;
    }

    public async Task<bool> Handle(DeletePaymentHistoryCommand request, CancellationToken cancellationToken)
    {
        var paymentHistory = await repository.SelectAsync(x => x.Id.Equals(request.Id))
            ?? throw new NotFoundException("Payment History was not found!");

        repository.Drop(paymentHistory);
        await repository.SaveAsync();

        return true;
    }
}
