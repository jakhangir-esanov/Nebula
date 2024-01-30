using Nebula.Application.DTOs;
using Nebula.Domain.Entities.Cars;

namespace Nebula.Application.Queries.Cars.GetCar;

public record GetAllCarsQuery : IRequest<IEnumerable<CarResultDto>>
{
}

public class GetAllCarsQueryHandler : IRequestHandler<GetAllCarsQuery, IEnumerable<CarResultDto>>
{
    private readonly IRepository<Car> repository;
    private readonly IMapper mapper;
    public GetAllCarsQueryHandler(IRepository<Car> repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public Task<IEnumerable<CarResultDto>> Handle(GetAllCarsQuery request, CancellationToken cancellationToken)
    {
        var cars = this.repository.SelectAll(includes: new[] { "Attachments" }).ToList();
        var res = mapper.Map<IEnumerable<CarResultDto>>(cars);
        return Task.FromResult(res);
    }
}
