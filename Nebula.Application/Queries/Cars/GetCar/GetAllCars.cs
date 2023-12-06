using Nebula.Application.DTOs;
using Nebula.Domain.Entities.Cars;

namespace Nebula.Application.Queries.Cars.GetCar;

public record GetAllCarsQuery : IRequest<IEnumerable<CarCategoryResultDto>>
{
}

public class GetAllCarsQueryHandler : IRequestHandler<GetAllCarsQuery, IEnumerable<CarCategoryResultDto>>
{
    private readonly IRepository<Car> repository;
    private readonly IMapper mapper;
    public GetAllCarsQueryHandler(IRepository<Car> repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public Task<IEnumerable<CarCategoryResultDto>> Handle(GetAllCarsQuery request, CancellationToken cancellationToken)
    {
        var cars = this.repository.SelectAll().ToList();
        var res = mapper.Map<IEnumerable<CarCategoryResultDto>>(cars);
        return Task.FromResult(res);
    }
}
