using Nebula.Domain.Entities.Rentals;

namespace Nebula.Application.Commands.Rentals.DeleteRental;

public record DeleteRentalCommand : IRequest<Rental>
{
    public DeleteRentalCommand(long id)
    {
        Id = id;
    }

    public long Id { get; set; }
}

public class DeleteRentalCommandHandler : IRequestHandler<DeleteRentalCommand, Rental>
{
    private readonly IRepository<Rental> repository;

    public DeleteRentalCommandHandler(IRepository<Rental> repository)
    {
        this.repository = repository;
    }

    public async Task<Rental> Handle(DeleteRentalCommand request, CancellationToken cancellationToken)
    {
        var rental = await repository.SelectAsync(x => x.Id.Equals(request.Id))
            ?? throw new NotFoundException("Rental was not found!");

        repository.Drop(rental);
        await repository.SaveAsync();

        return rental;
    }
}
