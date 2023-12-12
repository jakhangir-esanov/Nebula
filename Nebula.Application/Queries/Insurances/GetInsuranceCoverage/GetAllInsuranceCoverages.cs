using Nebula.Application.DTOs;
using Nebula.Domain.Entities.Insurances;

namespace Nebula.Application.Queries.Insurances.GetInsuranceCoverage;

public record GetAllInsuranceCoveragesQuery : IRequest<IEnumerable<InsuranceCoverageResultDto>>
{
}

public class GetAllInsuranceCoverageQueryHandler : IRequestHandler<GetAllInsuranceCoveragesQuery, 
    IEnumerable<InsuranceCoverageResultDto>>
{
    private readonly IRepository<InsuranceCoverage> repository;
    private readonly IMapper mapper;
    public GetAllInsuranceCoverageQueryHandler(IRepository<InsuranceCoverage> repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public Task<IEnumerable<InsuranceCoverageResultDto>> Handle(GetAllInsuranceCoveragesQuery request, CancellationToken cancellationToken)
    {
        var insuranceCoverages = this.repository.SelectAll().ToList();
        var res = mapper.Map<IEnumerable<InsuranceCoverageResultDto>>(insuranceCoverages);
        return Task.FromResult(res);
    }
}
