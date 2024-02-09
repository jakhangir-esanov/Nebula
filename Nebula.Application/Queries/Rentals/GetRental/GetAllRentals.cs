using Nebula.Application.DTOs;
using Nebula.Domain.Entities.Rentals;

namespace Nebula.Application.Queries.Rentals.GetRental;

public record GetAllRentalsQuery : IRequest<IEnumerable<RentalResultDto>>
{
}

public class GetAllRentalsQueryHandler : IRequestHandler<GetAllRentalsQuery, IEnumerable<RentalResultDto>>
{
    private readonly IRepository<Rental> repository;
    private readonly IMapper mapper;
    public GetAllRentalsQueryHandler(IRepository<Rental> repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<RentalResultDto>> Handle(GetAllRentalsQuery request, CancellationToken cancellationToken)
    {
        var rental = this.repository.SelectAll().ToList();
        var res = mapper.Map<IEnumerable<RentalResultDto>>(rental);
        return res;
    }
}
