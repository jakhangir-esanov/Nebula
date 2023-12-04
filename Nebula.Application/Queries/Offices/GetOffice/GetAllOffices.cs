using Nebula.Application.DTOs;
using Nebula.Domain.Entities.Offices;

namespace Nebula.Application.Queries.Offices.GetOffice;

public record GetAllOfficesQuery : IRequest<IEnumerable<OfficeResultDto>>
{
}

public class GetAllOfficesQueryHandler : IRequestHandler<GetAllOfficesQuery, IEnumerable<OfficeResultDto>>
{
    private readonly IRepository<Office> repository;
    private readonly IMapper mapper;
    public GetAllOfficesQueryHandler(IRepository<Office> repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public Task<IEnumerable<OfficeResultDto>> Handle(GetAllOfficesQuery request, CancellationToken cancellationToken)
    {
        var offices = this.repository.SelectAll().ToList();
        var res = mapper.Map<IEnumerable<OfficeResultDto>>(offices);
        return Task.FromResult(res);
    }
}
