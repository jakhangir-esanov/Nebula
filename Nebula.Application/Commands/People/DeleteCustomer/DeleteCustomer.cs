using Nebula.Application.Interfaces;
using Nebula.Domain.Entities.People;
using Nebula.Domain.Exceptions;

namespace Nebula.Application.Commands.People.DeleteCustomer;

public record DeleteCustomerCommand : IRequest<bool>
{
    public DeleteCustomerCommand(long id)
    {
        Id = id;
    }

    public long Id { get; set; }
}

public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, bool>
{
    private readonly IRepository<Customer> repository;

    public DeleteCustomerCommandHandler(IRepository<Customer> repository)
    {
        this.repository = repository;
    }

    public async Task<bool> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await repository.SelectAsync(x => x.Id.Equals(request.Id))
            ?? throw new NotFoundException("Customer was not found!");

        repository.Drop(customer);
        await repository.SaveAsync();

        return true;
    }
}
