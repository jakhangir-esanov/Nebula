using Nebula.Application.DTOs;
using Nebula.Domain.Entities.Payments;

namespace Nebula.Application.Queries.Payments.GetPaymentHistory;

public record GetAllPaymentHistoriesQuery : IRequest<IEnumerable<PaymentHistoryResultDto>>
{
}

public class GetAllPaymentHistoriesQueryHandler : IRequestHandler<GetAllPaymentHistoriesQuery, IEnumerable<PaymentHistoryResultDto>>
{
    private readonly IRepository<PaymentHistory> repository;
    private readonly IMapper mapper;
    public GetAllPaymentHistoriesQueryHandler(IRepository<PaymentHistory> repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public Task<IEnumerable<PaymentHistoryResultDto>> Handle(GetAllPaymentHistoriesQuery request, CancellationToken cancellationToken)
    {
        var paymentHistories = this.repository.SelectAll().ToList();
        var res = mapper.Map<IEnumerable<PaymentHistoryResultDto>>(paymentHistories);
        return Task.FromResult(res);
    }
}
