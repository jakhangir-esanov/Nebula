using Nebula.Application.DTOs;
using Nebula.Domain.Entities.Insurances;

namespace Nebula.Application.Queries.Insurances.GetInsuranceCoverage;

public record GetInsuranceCoverageQuery : IRequest<InsuranceCoverageResultDto>
{
    public long Id { get; set; }
}

public class GetInsuranceCoverageQueryHandler : IRequestHandler<GetInsuranceCoverageQuery, InsuranceCoverageResultDto>
{
    private readonly IRepository<InsuranceCoverage> repository;
    private readonly IMapper mapper;
    public GetInsuranceCoverageQueryHandler(IRepository<InsuranceCoverage> repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public async Task<InsuranceCoverageResultDto> Handle(GetInsuranceCoverageQuery request, CancellationToken cancellationToken)
    {
        var insuranceCoverage = await this.repository.SelectAsync(x => x.Id.Equals(request.Id), includes: new[] { "Insurances" } )
            ?? throw new NotFoundException("InsurancCoverage was not found!");

        return mapper.Map<InsuranceCoverageResultDto>(insuranceCoverage);
    }
}
