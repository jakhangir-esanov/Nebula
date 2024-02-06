using Nebula.Application.DTOs;
using Nebula.Domain.Entities.Cars;
using Nebula.Domain.Entities.Rentals;

namespace Nebula.Application.Queries.Rentals.GetRental;

public record GetRentalQuery : IRequest<RentalResultDto>
{
    public long Id { get; set; }
}

public class GetRentalQueryHandler : IRequestHandler<GetRentalQuery, RentalResultDto>
{
    private readonly IRepository<Rental> repository;
    private readonly IRepository<Car> carRepository;
    private readonly IMapper mapper;
    public GetRentalQueryHandler(IRepository<Rental> repository, IMapper mapper, IRepository<Car> carRepository)
    {
        this.repository = repository;
        this.mapper = mapper;
        this.carRepository = carRepository;
    }

    public async Task<RentalResultDto> Handle(GetRentalQuery request, CancellationToken cancellationToken)
    {
        var rental = await this.repository.SelectAsync(x => x.Id.Equals(request.Id))
            ?? throw new NotFoundException("Rental was not found!");

        if(rental.EndDate <= DateTime.Today)
        {
            var car = await this.carRepository.SelectAsync(x => x.Id.Equals(rental.CarId));
            car.IsAvailable = true;
            await this.carRepository.SaveAsync();
        }

        return mapper.Map<RentalResultDto>(rental);
    }
}
