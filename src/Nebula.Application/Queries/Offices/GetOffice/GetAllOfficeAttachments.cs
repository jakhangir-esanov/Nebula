using Nebula.Application.DTOs;
using Nebula.Domain.Entities.Attachments;
using Nebula.Domain.Entities.Offices;

namespace Nebula.Application.Queries.Offices.GetOffice;

public record GetAllOfficeAttachmentsQuery : IRequest<IEnumerable<AttachmentResultDto>>
{
    public long Id { get; set; }   
}

public class GetAllOfficeAttachmentsQueryHandler : IRequestHandler<GetAllOfficeAttachmentsQuery, IEnumerable<AttachmentResultDto>>
{
    private readonly IRepository<Office> officeRepository;
    private readonly IRepository<Attachment> attachmentRepository;
    private readonly IRepository<OfficeAttachment> officeAttachmentRepository;
    private readonly IMapper mapper;
    public GetAllOfficeAttachmentsQueryHandler(IRepository<OfficeAttachment> officeAttachmentRepository, IMapper mapper, IRepository<Attachment> attachmentRepository, IRepository<Office> officeRepository)
    {
        this.officeRepository = officeRepository;
        this.attachmentRepository = attachmentRepository;
        this.officeAttachmentRepository = officeAttachmentRepository;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<AttachmentResultDto>> Handle(GetAllOfficeAttachmentsQuery request, CancellationToken cancellationToken)
    {
        var office = await officeRepository.SelectAsync(x => x.Id.Equals(request.Id))
            ?? throw new NotFoundException("Office was not found!");

        var officeAttachment = officeAttachmentRepository.SelectAll().Where(x => x.Id.Equals(office.Id)).ToList();
        var attachment = attachmentRepository.SelectAll().ToList();

        var joinedData = officeAttachment.Join(attachment,
                                               o => o.OfficeId,
                                               a => a.Id,
                                               (office, attach) => attach
                                               ).ToList();

        return mapper.Map<IEnumerable<AttachmentResultDto>>(joinedData);
    }
}
