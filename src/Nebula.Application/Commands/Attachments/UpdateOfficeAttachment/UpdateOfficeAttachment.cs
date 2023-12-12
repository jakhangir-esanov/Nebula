using Nebula.Domain.Entities.Attachments;

namespace Nebula.Application.Commands.Attachments.UpdateOfficeAttachment;

public record UpdateOfficeAttachmentCommand : IRequest<OfficeAttachment>
{
    public UpdateOfficeAttachmentCommand(long id, long officeId, long attachmentId)
    {
        Id = id;
        OfficeId = officeId;
        AttachmentId = attachmentId;
    }

    public long Id { get; set; }
    public long OfficeId { get; set; }
    public long AttachmentId { get; set; }
}

public class UpdateOfficeAttachmentCommandHandler : IRequestHandler<UpdateOfficeAttachmentCommand, OfficeAttachment>
{
    private readonly IRepository<OfficeAttachment> repository;

    public UpdateOfficeAttachmentCommandHandler(IRepository<OfficeAttachment> repository)
    {
        this.repository = repository;
    }

    public async Task<OfficeAttachment> Handle(UpdateOfficeAttachmentCommand request, CancellationToken cancellationToken)
    {
        var officeAttachment = await repository.SelectAsync(x => x.Id.Equals(request.Id))
            ?? throw new NotFoundException("OfficeAttachment was not found!");

        officeAttachment.OfficeId = request.OfficeId;
        officeAttachment.AttachmentId = request.AttachmentId;

        repository.Update(officeAttachment);
        await repository.SaveAsync();

        return officeAttachment;
    }
}
