using Nebula.Application.DTOs;
using Nebula.Domain.Entities.Attachments;

namespace Nebula.Application.Queries.Offices.GetOffice;

public record GetAllOfficeAttachmentsQuery : IRequest<IEnumerable<AttachmentResultDto>>
{
    public long Id { get; set; }   
}

public class GetAllOfficeAttachmentsQueryHandler : IRequestHandler<GetAllOfficeAttachmentsQuery, IEnumerable<AttachmentResultDto>>
{
    private readonly IRepository<Attachment> attachmentRepository;
    private readonly IRepository<OfficeAttachment> officeRepository;
    private readonly IMapper mapper;
    public GetAllOfficeAttachmentsQueryHandler(IRepository<OfficeAttachment> officeRepository, IMapper mapper, IRepository<Attachment> attachmentRepository)
    {
        this.attachmentRepository = attachmentRepository;
        this.officeRepository = officeRepository;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<AttachmentResultDto>> Handle(GetAllOfficeAttachmentsQuery request, CancellationToken cancellationToken)
    {
        var officeAttachment = officeRepository.SelectAll().Where(x => x.Id.Equals(request.Id)).ToList();
        var attachment = attachmentRepository.SelectAll().ToList();

        var joinedData = officeAttachment.Join(attachment,
                                               o => o.OfficeId,
                                               a => a.Id,
                                               (office, attach) => attach
                                               ).ToList();

        return mapper.Map<IEnumerable<AttachmentResultDto>>(joinedData);
    }
}
