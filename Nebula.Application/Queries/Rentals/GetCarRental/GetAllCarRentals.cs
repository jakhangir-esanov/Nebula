using Nebula.Application.DTOs;
using Nebula.Domain.Entities.Rentals;

namespace Nebula.Application.Queries.Rentals.GetCarRental;

public record GetAllCarRentalsQuery : IRequest<IEnumerable<CarRentalResultDto>>
{
}

public class GetAllCarRentalsQueryHandler : IRequestHandler<GetAllCarRentalsQuery, IEnumerable<CarRentalResultDto>>
{
    private readonly IRepository<CarRental> repository;
    private readonly IMapper mapper;
    public GetAllCarRentalsQueryHandler(IRepository<CarRental> repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<CarRentalResultDto>> Handle(GetAllCarRentalsQuery request, CancellationToken cancellationToken)
    {
        var carRentals = this.repository.SelectAll().ToList();
        var res = mapper.Map<IEnumerable<CarRentalResultDto>>(carRentals);
        return res;
    }
}
