using Nebula.Application.Commands.Attachments.CreateAttachment;
using Nebula.Application.Commands.Attachments.CreateOfficeAttachment;
using Nebula.Application.DTOs;
using Nebula.Domain.Entities.Attachments;
using Nebula.Domain.Entities.Offices;

namespace Nebula.Application.Commands.Offices.UploadOfficeImage;

public record UploadOfficeImageCommand : IRequest<OfficeResultDto>
{
    public UploadOfficeImageCommand(long officeId, AttachmentCreationDto dto)
    {
        this.officeId = officeId;
        this.dto = dto;
    }

    public long officeId { get; set; }
    public AttachmentCreationDto dto { get; set; }
}

public class UploadOfficeImageCommandHandler : IRequestHandler<UploadOfficeImageCommand, OfficeResultDto>
{
    private readonly IRepository<Office> officeRepository;
    private readonly IRepository<OfficeAttachment> officeAttachmentRepository;
    private readonly IMediator mediator;
    private readonly IMapper mapper;
    public UploadOfficeImageCommandHandler(IRepository<Office> officeRepository, IRepository<OfficeAttachment>
        officeAttachmentRepository, IMediator mediator, IMapper mapper)
    {
        this.officeRepository = officeRepository;
        this.officeAttachmentRepository = officeAttachmentRepository;
        this.mediator = mediator;
        this.mapper = mapper;
    }

    public async Task<OfficeResultDto> Handle(UploadOfficeImageCommand request, CancellationToken cancellationToken)
    {
        var office = await this.officeRepository.SelectAsync(x => x.Id.Equals(request.officeId))
            ?? throw new NotFoundException("Office was not found!");

        var officeAttachment = this.officeAttachmentRepository.SelectAll().Where(x => x.OfficeId.Equals(office.Id)).ToList();
        if (officeAttachment.Count() == 10)
            throw new CustomException(429, "Out of limit image!");

        var createdAttachment = await this.mediator.Send(new CreateAttachmentCommand("OfficeFile", request.dto));

        await this.mediator.Send(new CreateOfficeAttachmentCommand(office.Id, createdAttachment.Id));
        createdAttachment.OfficeAttachments = officeAttachment;
        office.OfficeAttachments = officeAttachment;

        return mapper.Map<OfficeResultDto>(office);
    }
}
