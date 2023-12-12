using Nebula.Application.DTOs;
using Nebula.Domain.Entities.Cars;

namespace Nebula.Application.Queries.Cars.GetCarCategory;

public record GetCarCategoryQuery : IRequest<CarCategoryResultDto>
{
    public long Id { get; set; }
}

public class GetCarCategoryQueryHandler : IRequestHandler<GetCarCategoryQuery, CarCategoryResultDto>
{
    private readonly IRepository<CarCategory> repository;
    private readonly IMapper mapper;
    public GetCarCategoryQueryHandler(IRepository<CarCategory> repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public async Task<CarCategoryResultDto> Handle(GetCarCategoryQuery request, CancellationToken cancellationToken)
    {
        var carCategory = await this.repository.SelectAsync(x => x.Id.Equals(request.Id), includes: new[] { "Cars" })
            ?? throw new NotFoundException("CarCategory was not found!");

        return mapper.Map<CarCategoryResultDto>(carCategory);
    }
}
