
using Nebula.Application.Commands.Attachments.DeleteAttachment;
using Nebula.Domain.Entities.Attachments;
using Nebula.Domain.Entities.Offices;

namespace Nebula.Application.Commands.Offices.DeleteOfficeImage;

public record DeleteOfficeImageCommand : IRequest<bool>
{
    public DeleteOfficeImageCommand(long officeId, long attachmentId)
    {
        this.officeId = officeId;
        this.attachmentId = attachmentId;
    }

    public long officeId { get; set; }
    public long attachmentId { get; set; }
}


public class DeleteOfficeImageCommandHandler : IRequestHandler<DeleteOfficeImageCommand, bool>
{
    private readonly IRepository<Office> officeRepository;
    private readonly IRepository<OfficeAttachment> officeAttachmentRepository;
    private readonly IMediator mediator;
    public DeleteOfficeImageCommandHandler(IRepository<Office> officeRepository, IRepository<OfficeAttachment> 
        officeAttachmentRepository, IMediator mediator)
    {
        this.officeRepository = officeRepository;
        this.officeAttachmentRepository = officeAttachmentRepository;
        this.mediator = mediator;
    }

    public async Task<bool> Handle(DeleteOfficeImageCommand request, CancellationToken cancellationToken)
    {
        var office = await this.officeRepository.SelectAsync(x => x.Id.Equals(request.officeId))
            ?? throw new NotFoundException("Office was not found!");

        var officeAttachment = await this.officeAttachmentRepository.SelectAsync(x => x.OfficeId.Equals(request.officeId)
            && x.AttachmentId.Equals(request.attachmentId))
            ?? throw new NotFoundException("Attachment was not found!");

        officeAttachmentRepository.Drop(officeAttachment);
        await this.mediator.Send(new DeleteAttachmentCommand(request.attachmentId));

        return true;
    }
}
