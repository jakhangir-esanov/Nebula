using Nebula.Application.DTOs;
using Nebula.Domain.Entities.Offices;

namespace Nebula.Application.Queries.Offices.GetOffice;

public record GetOfficeQuery : IRequest<OfficeResultDto>
{
    public long Id { get; set; }
}

public class GetOfficeQueryHandler : IRequestHandler<GetOfficeQuery, OfficeResultDto>
{
    private readonly IRepository<Office> repository;
    private readonly IMapper mapper;
    public GetOfficeQueryHandler(IRepository<Office> repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public async Task<OfficeResultDto> Handle(GetOfficeQuery request, CancellationToken cancellationToken)
    {
        var office = await this.repository.SelectAsync(x => x.Id.Equals(request.Id))
            ?? throw new NotFoundException("Office was not found!");

        return mapper.Map<OfficeResultDto>(office); 
    }
}
