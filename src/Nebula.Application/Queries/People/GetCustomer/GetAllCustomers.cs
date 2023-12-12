using Nebula.Application.DTOs;
using Nebula.Domain.Entities.People;

namespace Nebula.Application.Queries.People.GetCustomer;

public record GetAllCustomersQuery : IRequest<IEnumerable<CustomerResultDto>>
{
}

public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, IEnumerable<CustomerResultDto>>
{
    private readonly IRepository<Customer> repository;
    private readonly IMapper mapper;
    public GetAllCustomersQueryHandler(IRepository<Customer> repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public Task<IEnumerable<CustomerResultDto>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
    {
        var customers = this.repository.SelectAll().ToList();
        var res = mapper.Map<IEnumerable<CustomerResultDto>>(customers);
        return Task.FromResult(res);
    }
}
