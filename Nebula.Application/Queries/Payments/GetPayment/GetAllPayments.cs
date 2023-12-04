using Nebula.Application.DTOs;
using Nebula.Domain.Entities.Payments;

namespace Nebula.Application.Queries.Payments.GetPayment;

public record GetAllPaymentsQuery : IRequest<IEnumerable<PaymentResultDto>>
{
}

public class GetAllPaymentQueryHandler : IRequestHandler<GetAllPaymentsQuery, IEnumerable<PaymentResultDto>>
{
    private readonly IRepository<Payment> repository;
    private readonly IMapper mapper;
    public GetAllPaymentQueryHandler(IRepository<Payment> repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public Task<IEnumerable<PaymentResultDto>> Handle(GetAllPaymentsQuery request, CancellationToken cancellationToken)
    {
        var payments = this.repository.SelectAll().ToList();
        var res = mapper.Map<IEnumerable<PaymentResultDto>>(payments);
        return Task.FromResult(res);
    }
}
