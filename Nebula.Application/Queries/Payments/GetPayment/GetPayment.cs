using Nebula.Application.DTOs;
using Nebula.Domain.Entities.Payments;

namespace Nebula.Application.Queries.Payments.GetPayment;

public record GetPaymentQuery : IRequest<PaymentResultDto>
{
    public long Id { get; set; }    
}

public class GetPaymentQueryHandler : IRequestHandler<GetPaymentQuery, PaymentResultDto>
{
    private readonly IRepository<Payment> repository;
    private readonly IMapper mapper;
    public GetPaymentQueryHandler(IRepository<Payment> repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public async Task<PaymentResultDto> Handle(GetPaymentQuery request, CancellationToken cancellationToken)
    {
        var payment = await this.repository.SelectAsync(x => x.Id.Equals(request.Id))
            ?? throw new NotFoundException("Payment was not found!");

        return mapper.Map<PaymentResultDto>(payment);
    }
}
