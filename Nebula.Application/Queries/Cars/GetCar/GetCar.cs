using Nebula.Application.DTOs;
using Nebula.Domain.Entities.Cars;

namespace Nebula.Application.Queries.Cars.GetCar;

public record GetCarQuery : IRequest<CarResultDto>
{
    public long Id { get; set; }
}

public class GetCarQueryHandler : IRequestHandler<GetCarQuery, CarResultDto>
{
    private readonly IRepository<Car> repository;
    private readonly IMapper mapper;
    public GetCarQueryHandler(IRepository<Car> repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public async Task<CarResultDto> Handle(GetCarQuery request, CancellationToken cancellationToken)
    {
        var car = await this.repository.SelectAsync(x => x.Id.Equals(request.Id), includes: new[] { "Attachments" })
            ?? throw new NotFoundException("Car was not found!");

        return mapper.Map<CarResultDto>(car);
    }
}
