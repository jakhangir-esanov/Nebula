using Nebula.Application.DTOs;
using Nebula.Domain.Entities.Attachments;

namespace Nebula.Application.Queries.Attachments.GetOfficeAttachment;

public record GetOfficeAttachmentQuery : IRequest<OfficeAttachmentResultDto>
{
    public long Id { get; set; }
}

public class GetOfficeAttachmentQueryHandler : IRequestHandler<GetOfficeAttachmentQuery, OfficeAttachmentResultDto>
{
    private readonly IRepository<OfficeAttachment> repository;
    private readonly IMapper mapper;
    public GetOfficeAttachmentQueryHandler(IRepository<OfficeAttachment> repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public async Task<OfficeAttachmentResultDto> Handle(GetOfficeAttachmentQuery request, CancellationToken cancellationToken)
    {
        var officeAttachment = await this.repository.SelectAsync(x => x.Id.Equals(request.Id))
            ?? throw new NotFoundException("Office Attachment was not found!");

        return this.mapper.Map<OfficeAttachmentResultDto>(officeAttachment);
    }
}
