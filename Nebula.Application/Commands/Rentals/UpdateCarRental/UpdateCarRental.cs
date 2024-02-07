using Nebula.Domain.Entities.Rentals;

namespace Nebula.Application.Commands.Rentals.UpdateCarRental;

public record UpdateCarRentalCommand : IRequest<CarRental>
{
    public UpdateCarRentalCommand(long id, long carId, long rentalId)
    {
        Id = id;
        CarId = carId;
        RentalId = rentalId;
    }

    public long Id { get; set; }
    public long CarId { get; set; }
    public long RentalId { get; set; }
}

public class UpdateCarRentalCommandHandler : IRequestHandler<UpdateCarRentalCommand, CarRental>
{
    private readonly IRepository<CarRental> repository;

    public UpdateCarRentalCommandHandler(IRepository<CarRental> repository)
    {
        this.repository = repository;
    }

    public async Task<CarRental> Handle(UpdateCarRentalCommand request, CancellationToken cancellationToken)
    {
        var carRental = await this.repository.SelectAsync(x => x.Id.Equals(request.Id))
            ?? throw new NotFoundException("Rental was not found!");

        carRental.CarId = request.CarId;
        carRental.RentalId = request.RentalId;

        this.repository.Update(carRental);
        await this.repository.SaveAsync();

        return carRental;
    }
}
