using Nebula.Application.DTOs;
using Nebula.Domain.Entities.Payments;

namespace Nebula.Application.Queries.Payments.GetPaymentHistory;

public record GetPaymentHistoryQuery : IRequest<PaymentHistoryResultDto>
{
    public long Id { get; set; }
}

public class GetPaymentHistoryQueryHandler : IRequestHandler<GetPaymentHistoryQuery, PaymentHistoryResultDto>
{
    private readonly IRepository<PaymentHistory> repository;
    private readonly IMapper mapper;
    public GetPaymentHistoryQueryHandler(IRepository<PaymentHistory> repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public async Task<PaymentHistoryResultDto> Handle(GetPaymentHistoryQuery request, CancellationToken cancellationToken)
    {
        var paymentHistory = await this.repository.SelectAsync(x => x.Id.Equals(request.Id))
            ?? throw new NotFoundException("PaymentHistory was not found!");

        return mapper.Map<PaymentHistoryResultDto>(paymentHistory);
    }
}
