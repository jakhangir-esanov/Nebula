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

    public UpdateRentalCommandHandler(IRepository<Rental> repository)
    {
        this.repository = repository;
    }

    public async Task<Rental> Handle(UpdateRentalCommand request, CancellationToken cancellationToken)
    {
        var rental = await repository.SelectAsync(x => x.Id.Equals(request.Id))
            ?? throw new NotFoundException("Rental was not found!");

        rental.CustomerId = request.CustomerId;
        rental.CarId = request.CarId;
        rental.StartDate = request.StartDate;
        rental.EndDate = request.EndDate;

        repository.Update(rental);
        await repository.SaveAsync();

        return rental;
    }
}
