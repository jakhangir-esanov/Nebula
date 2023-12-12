using Nebula.Application.DTOs;
using Nebula.Domain.Entities.Insurances;

namespace Nebula.Application.Queries.Insurances.GetInsurance;

public record GetAllInsurancesQuery : IRequest<IEnumerable<InsuranceResultDto>>
{
}

public class GetAllInsurancesQueryHandler : IRequestHandler<GetAllInsurancesQuery, IEnumerable<InsuranceResultDto>>
{
    private readonly IRepository<Insurance> repository;
    private readonly IMapper mapper;
    public GetAllInsurancesQueryHandler(IRepository<Insurance> repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public Task<IEnumerable<InsuranceResultDto>> Handle(GetAllInsurancesQuery request, CancellationToken cancellationToken)
    {
        var insurances = this.repository.SelectAll().ToList();
        var res = mapper.Map<IEnumerable<InsuranceResultDto>>(insurances);
        return Task.FromResult(res);
    }
}
