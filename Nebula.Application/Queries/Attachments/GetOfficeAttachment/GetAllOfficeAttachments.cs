using Nebula.Application.DTOs;
using Nebula.Domain.Entities.Attachments;

namespace Nebula.Application.Queries.Attachments.GetOfficeAttachment;

public record GetAllOfficeAttachmentsQuery : IRequest<IEnumerable<OfficeAttachmentResultDto>>
{
}

public class GetAllOfficeAttachmentsQueryHandler : IRequestHandler<GetAllOfficeAttachmentsQuery, IEnumerable<OfficeAttachmentResultDto>>
{
    private readonly IRepository<OfficeAttachment> repository;
    private readonly IMapper mapper;
    public GetAllOfficeAttachmentsQueryHandler(IRepository<OfficeAttachment> repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public Task<IEnumerable<OfficeAttachmentResultDto>> Handle(GetAllOfficeAttachmentsQuery request, CancellationToken cancellationToken)
    {
        var officeAttachments = this.repository.SelectAll().ToList();
        var res = mapper.Map<IEnumerable<OfficeAttachmentResultDto>>(officeAttachments);
        return Task.FromResult(res);
    }
}
