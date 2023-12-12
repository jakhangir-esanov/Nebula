using Nebula.Application.DTOs;
using Nebula.Domain.Entities.Insurances;

namespace Nebula.Application.Queries.Insurances.GetInsurance;

public record GetInsuranceQuery : IRequest<InsuranceResultDto>
{
    public long Id { get; set; }
}

public class GetInsuranceQueryHandler : IRequestHandler<GetInsuranceQuery, InsuranceResultDto>
{
    private readonly IRepository<Insurance> repository;
    private readonly IMapper mapper;
    public GetInsuranceQueryHandler(IRepository<Insurance> repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public async Task<InsuranceResultDto> Handle(GetInsuranceQuery request, CancellationToken cancellationToken)
    {
        var insurance = await this.repository.SelectAsync(x => x.Id.Equals(request.Id))
            ?? throw new NotFoundException("Insurance was not found!");

        return mapper.Map<InsuranceResultDto>(insurance);
    }
}
