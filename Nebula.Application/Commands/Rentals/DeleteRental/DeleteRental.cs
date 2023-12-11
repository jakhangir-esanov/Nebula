using Nebula.Domain.Entities.Cars;
using Nebula.Domain.Entities.Rentals;

namespace Nebula.Application.Commands.Rentals.DeleteRental;

public record DeleteRentalCommand : IRequest<bool>
{
    public DeleteRentalCommand(long id)
    {
        Id = id;
    }

    public long Id { get; set; }
}

public class DeleteRentalCommandHandler : IRequestHandler<DeleteRentalCommand, bool>
{
    private readonly IRepository<Rental> repository;
    private readonly IRepository<Car> carRepository;
    public DeleteRentalCommandHandler(IRepository<Rental> repository, IRepository<Car> carRepository)
    {
        this.repository = repository;
        this.carRepository = carRepository;
    }

    public async Task<bool> Handle(DeleteRentalCommand request, CancellationToken cancellationToken)
    {
        var rental = await repository.SelectAsync(x => x.Id.Equals(request.Id))
                    ?? throw new NotFoundException("Rental was not found!");

        var car = await this.carRepository.SelectAsync(x => x.Id.Equals(rental.CarId));

        repository.Drop(rental);
        await repository.SaveAsync();

        car.IsAvailable = true;
        this.carRepository.Update(car);
        await this.carRepository.SaveAsync();

        return true;
    }
}
