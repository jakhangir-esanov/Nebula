using Nebula.Application.DTOs;
using Nebula.Domain.Entities.Attachments;

namespace Nebula.Application.Queries.Attachments;

public record GetAllAttachmentsQuery : IRequest<IEnumerable<AttachmentResultDto>>
{
}

public class GetAllAttachmentsQueryHandler : IRequestHandler<GetAllAttachmentsQuery, IEnumerable<AttachmentResultDto>>
{
    private readonly IRepository<Attachment> repository;
    private readonly IMapper mapper;
    public GetAllAttachmentsQueryHandler(IRepository<Attachment> repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public Task<IEnumerable<AttachmentResultDto>> Handle(GetAllAttachmentsQuery request, CancellationToken cancellationToken)
    {
        var attachments = this.repository.SelectAll().ToList();
        var res = mapper.Map<IEnumerable<AttachmentResultDto>>(attachments);
        return Task.FromResult(res);
    }
}
