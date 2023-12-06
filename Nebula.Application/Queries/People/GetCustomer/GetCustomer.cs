using Nebula.Application.DTOs;
using Nebula.Domain.Entities.People;

namespace Nebula.Application.Queries.People.GetCustomer;

public record GetCustomerQuery : IRequest<CustomerResultDto>
{
    public long Id { get; set; }
}

public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, CustomerResultDto>
{
    private readonly IRepository<Customer> repository;
    private readonly IMapper mapper;
    public GetCustomerQueryHandler(IRepository<Customer> repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public async Task<CustomerResultDto> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
    {
        var customer = await this.repository.SelectAsync(x => x.Id.Equals(request.Id), new[] { "PaymentHistories" })
                    ?? throw new NotFoundException("Customer was not found!");

        return mapper.Map<CustomerResultDto>(customer);
    }
}
