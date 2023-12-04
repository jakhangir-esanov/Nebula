using Nebula.Application.DTOs;
using Nebula.Domain.Entities.Feedbacks;

namespace Nebula.Application.Queries.Feedbacks.GetFeedback;

public record GetFeedbackQuery : IRequest<FeedbackResultDto>
{
    public long Id { get; set; }
}

public class GetFeedbackQueryHandler : IRequestHandler<GetFeedbackQuery, FeedbackResultDto>
{
    private readonly IRepository<Feedback> repository;
    private readonly IMapper mapper;
    public GetFeedbackQueryHandler(IRepository<Feedback> repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public async Task<FeedbackResultDto> Handle(GetFeedbackQuery request, CancellationToken cancellationToken)
    {
        var feedback = await this.repository.SelectAsync(x => x.Id.Equals(request.Id))
            ?? throw new NotFoundException("Feedback was not found!");

        return mapper.Map<FeedbackResultDto>(feedback);
    }
}
