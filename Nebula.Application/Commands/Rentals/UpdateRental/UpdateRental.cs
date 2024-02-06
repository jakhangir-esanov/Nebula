using Nebula.Domain.Entities.Cars;
using Nebula.Domain.Entities.Rentals;

namespace Nebula.Application.Commands.Rentals.UpdateRental;

public record UpdateRentalCommand : IRequest<Rental>
{
    public UpdateRentalCommand(long id, long customerId, long carId, DateTime startDate, DateTime endDate)
    {
        Id = id;
        CustomerId = customerId;
        CarId = carId;
        StartDate = startDate;
        EndDate = endDate;
    }

    public long Id { get; set; }
    public long CustomerId { get; set; }
    public long CarId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}

public class UpdateRentalCommandHandler : IRequestHandler<UpdateRentalCommand, Rental>
{
    private readonly IRepository<Rental> repository;
    private readonly IRepository<Car> carRepository;
    public UpdateRentalCommandHandler(IRepository<Rental> repository, IRepository<Car> carRepository)
    {
        this.repository = repository;
        this.carRepository = carRepository;
    }

    public async Task<Rental> Handle(UpdateRentalCommand request, CancellationToken cancellationToken)
    {
        var rental = await repository.SelectAsync(x => x.Id.Equals(request.Id))
            ?? throw new NotFoundException("Rental was not found!");

        var car = await carRepository.SelectAsync(x => x.Id.Equals(request.CarId))
           ?? throw new NotFoundException("Car was not found!");

        rental.CustomerId = request.CustomerId;
        rental.CarId = request.CarId;
        rental.TotalPrice = (request.EndDate - request.StartDate).Days * car.Price;
        rental.StartDate = request.StartDate;
        rental.EndDate = request.EndDate;

        repository.Update(rental);
        await repository.SaveAsync();

        car.IsAvailable = false;
        this.carRepository.Update(car);
        await this.carRepository.SaveAsync();

        return rental;
    }
}
