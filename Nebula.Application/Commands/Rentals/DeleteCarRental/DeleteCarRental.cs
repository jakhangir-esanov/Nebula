
using Nebula.Domain.Entities.Rentals;

namespace Nebula.Application.Commands.Rentals.DeleteCarRental;

public record DeleteCarRentalCommand : IRequest<bool>
{
    public DeleteCarRentalCommand(long id)
    {
        Id = id;
    }

    public long Id { get; set; }
}

public class DeleteCarRentalCommandHandler : IRequestHandler<DeleteCarRentalCommand, bool>
{
    private readonly IRepository<CarRental> repository;

    public DeleteCarRentalCommandHandler(IRepository<CarRental> repository)
    {
        this.repository = repository;
    }

    public async Task<bool> Handle(DeleteCarRentalCommand request, CancellationToken cancellationToken)
    {
        var carRental = await this.repository.SelectAsync(x => x.Id.Equals(request.Id))
            ?? throw new NotFoundException("CarRental was not found!");

        this.repository.Drop(carRental);
        await this.repository.SaveAsync();

        return true;
    }
}
