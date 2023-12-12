using Nebula.Domain.Entities.Cars;
using Nebula.Domain.Entities.Rentals;

namespace Nebula.Application.Commands.Rentals.CreateRental;

public record CreateRentalCommand : IRequest<Rental>
{
    public CreateRentalCommand(long customerId, long carId, DateTime startDate, DateTime endDate)
    {
        CustomerId = customerId;
        CarId = carId;
        StartDate = startDate;
        EndDate = endDate;
    }

    public long CustomerId { get; set; }
    public long CarId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}

public class CreateRentalCommandHandler : IRequestHandler<CreateRentalCommand, Rental>
{
    private readonly IRepository<Rental> repository;
    private readonly IRepository<Car> carRepository;
    public CreateRentalCommandHandler(IRepository<Rental> repository, IRepository<Car> carRepository)
    {
        this.repository = repository;
        this.carRepository = carRepository;
    }

    public async Task<Rental> Handle(CreateRentalCommand request, CancellationToken cancellationToken)
    {
        var rental = await repository.SelectAsync(x => x.CarId.Equals(request.CarId));
        if (rental is not null)
            throw new AlreadyExistException("Rental is already exist!");

        var car = await carRepository.SelectAsync(x => x.Id.Equals(request.CarId))
            ?? throw new NotFoundException("Car was not found!");

        var newRental = new Rental()
        {
            CustomerId = request.CustomerId,
            CarId = request.CarId,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
        };

        await repository.InsertAsync(newRental);
        await repository.SaveAsync();

        car.IsAvailable = false;
        carRepository.Update(car);
        await this.carRepository.SaveAsync();

        return newRental;
    }
}
