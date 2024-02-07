using Nebula.Application.DTOs;
using Nebula.Domain.Entities.Rentals;

namespace Nebula.Application.Queries.Rentals.GetCarRental;

public record GetCarRentalQuery : IRequest<CarRentalResultDto>
{
    public long Id { get; set; }
}

public class GetCarRentalQueryHandler : IRequestHandler<GetCarRentalQuery, CarRentalResultDto>
{
    private readonly IRepository<CarRental> repository;
    private readonly IMapper mapper;
    public GetCarRentalQueryHandler(IRepository<CarRental> repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public async Task<CarRentalResultDto> Handle(GetCarRentalQuery request, CancellationToken cancellationToken)
    {
        var carRental = await this.repository.SelectAsync(x => x.Id.Equals(request.Id), includes: new[] { "CarRentals " })
            ?? throw new NotFoundException("CarRental was not found!");

        return mapper.Map<CarRentalResultDto>(carRental);
    }
}
