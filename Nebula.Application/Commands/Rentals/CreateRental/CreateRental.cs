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
    public CreateRentalCommandHandler(IRepository<Rental> repository)
    {
        this.repository = repository;
    }

    public async Task<Rental> Handle(CreateRentalCommand request, CancellationToken cancellationToken)
    {
        var rental = await repository.SelectAsync(x => x.CustomerId.Equals(request.CustomerId));
        if (rental is not null && rental.EndDate >= DateTime.Today)
            throw new AlreadyExistException("Rental is already exist!");

        var newRental = new Rental()
        {
            CustomerId = request.CustomerId,
            CarId = request.CarId,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
        };

        await repository.InsertAsync(newRental);
        await repository.SaveAsync();

        return newRental;
    }
}
