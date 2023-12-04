
using Nebula.Domain.Entities.Payments;

namespace Nebula.Application.Commands.Payments.DeletePayment;

public record DeletePaymentCommand : IRequest<bool>
{
    public DeletePaymentCommand(long id)
    {
        Id = id;
    }

    public long Id { get; set; }
}

public class DeletePaymentCommandHandler : IRequestHandler<DeletePaymentCommand, bool>
{
    private readonly IRepository<Payment> repository;

    public DeletePaymentCommandHandler(IRepository<Payment> repository)
    {
        this.repository = repository;
    }

    public async Task<bool> Handle(DeletePaymentCommand request, CancellationToken cancellationToken)
    {
        var payment = await repository.SelectAsync(x => x.Id.Equals(request.Id))
            ?? throw new NotFoundException("Payment was not found!");

        repository.Drop(payment);
        await repository.SaveAsync();

        return true;
    }
}
