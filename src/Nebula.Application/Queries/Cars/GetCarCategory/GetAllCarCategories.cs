using Nebula.Application.DTOs;
using Nebula.Domain.Entities.Cars;

namespace Nebula.Application.Queries.Cars.GetCarCategory;

public record GetAllCarCategoriesQuery : IRequest<IEnumerable<CarCategoryResultDto>>
{
}

public class GetAllCarCategoriesQueryHandler : IRequestHandler<GetAllCarCategoriesQuery, IEnumerable<CarCategoryResultDto>>
{
    private readonly IRepository<CarCategory> repository;
    private readonly IMapper mapper;
    public GetAllCarCategoriesQueryHandler(IRepository<CarCategory> repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public Task<IEnumerable<CarCategoryResultDto>> Handle(GetAllCarCategoriesQuery request, CancellationToken cancellationToken)
    {
        var carCategories = this.repository.SelectAll().ToList();
        var res = mapper.Map<IEnumerable<CarCategoryResultDto>>(carCategories);
        return Task.FromResult(res);
    }
}
