using Nebula.Application.Commands.Attachments.UpdateAttachment;
using Nebula.Application.DTOs;
using Nebula.Domain.Entities.Attachments;
using Nebula.Domain.Entities.Offices;

namespace Nebula.Application.Commands.Offices.UpdateOfficeImage;

public record UpdateOfficeImageCommnd : IRequest<OfficeResultDto>
{
    public UpdateOfficeImageCommnd(long officeId, long attachmentId, AttachmentCreationDto dto)
    {
        this.officeId = officeId;
        this.attachmentId = attachmentId;
        this.dto = dto;
    }

    public long officeId { get; set; }
    public long attachmentId { get; set; }
    public AttachmentCreationDto dto { get; set; }
}

public class UpdateOfficeImageCommandHandler : IRequestHandler<UpdateOfficeImageCommnd, OfficeResultDto>
{
    private readonly IRepository<Office> officeRepository;
    private readonly IRepository<OfficeAttachment> officeAttachmentRepository;
    private readonly IMediator mediator;
    private readonly IMapper mapper;
    public UpdateOfficeImageCommandHandler(IRepository<Office> officeRepository, IRepository<OfficeAttachment>
        officeAttachmentRepository, IMediator mediator, IMapper mapper)
    {
        this.officeRepository = officeRepository;
        this.officeAttachmentRepository = officeAttachmentRepository;
        this.mediator = mediator;
        this.mapper = mapper;
    }

    public async Task<OfficeResultDto> Handle(UpdateOfficeImageCommnd request, CancellationToken cancellationToken)
    {
        var office = await this.officeRepository.SelectAsync(x => x.Id.Equals(request.officeId))
            ?? throw new NotFoundException("Office was not found!");

        var officeAttachment = await this.officeAttachmentRepository.SelectAsync(x => x.OfficeId.Equals(request.officeId)
            && x.AttachmentId.Equals(request.attachmentId))
            ?? throw new NotFoundException("Attachment was not found!");

        await this.mediator.Send(new UpdateAttachmentCommand("OfficeFile", request.attachmentId, request.dto));

        return mapper.Map<OfficeResultDto>(office);
    }
}
