using Nebula.Application.DTOs;
using Nebula.Domain.Entities.Attachments;

namespace Nebula.Application.Queries.Attachments.GetCarAttachment;

public class GetAllCarAttachmentsQuery : IRequest<IEnumerable<CarAttachmentResultDto>>
{
}

public class GetAllCarAttachmentsQueryHandler : IRequestHandler<GetAllCarAttachmentsQuery, IEnumerable<CarAttachmentResultDto>>
{
    private readonly IRepository<CarAttachment> repository;
    private readonly IMapper mapper;
    public GetAllCarAttachmentsQueryHandler(IRepository<CarAttachment> repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public Task<IEnumerable<CarAttachmentResultDto>> Handle(GetAllCarAttachmentsQuery request, CancellationToken cancellationToken)
    {
        var carAttachments = this.repository.SelectAll().ToList();
        var res = mapper.Map<IEnumerable<CarAttachmentResultDto>>(carAttachments);
        return Task.FromResult(res);
    }
}
