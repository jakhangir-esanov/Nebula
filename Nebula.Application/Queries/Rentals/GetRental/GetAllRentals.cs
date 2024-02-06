using Nebula.Application.DTOs;
using Nebula.Domain.Entities.Cars;
using Nebula.Domain.Entities.Rentals;

namespace Nebula.Application.Queries.Rentals.GetRental;

public record GetAllRentalsQuery : IRequest<IEnumerable<RentalResultDto>>
{
}

public class GetAllRentalsQueryHandler : IRequestHandler<GetAllRentalsQuery, IEnumerable<RentalResultDto>>
{
    private readonly IRepository<Rental> repository;
    private readonly IRepository<Car> carRepository;
    private readonly IMapper mapper;
    public GetAllRentalsQueryHandler(IRepository<Rental> repository, IMapper mapper, IRepository<Car> carRepository)
    {
        this.repository = repository;
        this.mapper = mapper;
        this.carRepository = carRepository;
    }

    public async Task<IEnumerable<RentalResultDto>> Handle(GetAllRentalsQuery request, CancellationToken cancellationToken)
    {
        var rental = this.repository.SelectAll().ToList();
        
        foreach(var item in rental)
        {
            if(item.EndDate <= DateTime.Today)
            {
                var car = await this.carRepository.SelectAsync(x => x.Id.Equals(item.CarId));
                car.IsAvailable = true;
                await this.carRepository.SaveAsync();
            }
        }

        var res = mapper.Map<IEnumerable<RentalResultDto>>(rental);
        return res;
    }
}
