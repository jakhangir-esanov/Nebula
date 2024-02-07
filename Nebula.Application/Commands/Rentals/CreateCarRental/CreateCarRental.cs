using Nebula.Domain.Entities.Rentals;

namespace Nebula.Application.Commands.Rentals.CreateCarRental;

public record CreateCarRentalCommand : IRequest<CarRental>
{
    public CreateCarRentalCommand(long carId, long rentalId)
    {
        CarId = carId;
        RentalId = rentalId;
    }

    public long CarId { get; set; }
    public long RentalId { get; set; }
}

public class CreateCarRentalCommandHandler : IRequestHandler<CreateCarRentalCommand, CarRental>
{
    private readonly IRepository<CarRental> repository;

    public CreateCarRentalCommandHandler(IRepository<CarRental> repository)
    {
        this.repository = repository;
    }

    public async Task<CarRental> Handle(CreateCarRentalCommand request, CancellationToken cancellationToken)
    {
        var newCarRental = new CarRental()
        {
            CarId = request.CarId,
            RentalId = request.RentalId
        };

        await this.repository.InsertAsync(newCarRental);
        await this.repository.SaveAsync();

        return newCarRental;
    }
}
