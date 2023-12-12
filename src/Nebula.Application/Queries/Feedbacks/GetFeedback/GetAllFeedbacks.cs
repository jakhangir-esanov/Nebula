using Nebula.Application.DTOs;
using Nebula.Domain.Entities.Feedbacks;

namespace Nebula.Application.Queries.Feedbacks.GetFeedback;

public record GetAllFeedbacksQuery : IRequest<IEnumerable<FeedbackResultDto>>
{
}

public class GetAllFeedbacksQueryHandler : IRequestHandler<GetAllFeedbacksQuery, IEnumerable<FeedbackResultDto>>
{
    private readonly IRepository<Feedback> repository;
    private readonly IMapper mapper;
    public GetAllFeedbacksQueryHandler(IRepository<Feedback> repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public Task<IEnumerable<FeedbackResultDto>> Handle(GetAllFeedbacksQuery request, CancellationToken cancellationToken)
    {
        var feedbacks = this.repository.SelectAll().ToList();
        var var = mapper.Map<IEnumerable<FeedbackResultDto>>(feedbacks);
        return Task.FromResult(var);
    }
}
