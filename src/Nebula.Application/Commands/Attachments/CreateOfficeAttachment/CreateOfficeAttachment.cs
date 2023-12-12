using Nebula.Domain.Entities.Attachments;
using Nebula.Domain.Entities.Offices;

namespace Nebula.Application.Commands.Attachments.CreateOfficeAttachment;

public record CreateOfficeAttachmentCommand : IRequest<OfficeAttachment>
{
    public CreateOfficeAttachmentCommand(long officeId, long attachmentId)
    {
        OfficeId = officeId;
        AttachmentId = attachmentId;
    }

    public long OfficeId { get; set; }
    public long AttachmentId { get; set; }
}

public class CreateOfficeAttachmentCommandHandler : IRequestHandler<CreateOfficeAttachmentCommand, OfficeAttachment>
{
    private readonly IRepository<OfficeAttachment> repository;

    public CreateOfficeAttachmentCommandHandler(IRepository<OfficeAttachment> repository)
    {
        this.repository = repository;
    }

    public async Task<OfficeAttachment> Handle(CreateOfficeAttachmentCommand request, CancellationToken cancellationToken)
    {
        var newOfficeAttachment = new OfficeAttachment()
        {
            OfficeId = request.OfficeId,
            AttachmentId = request.AttachmentId
        };

        await repository.InsertAsync(newOfficeAttachment);
        await repository.SaveAsync();

        return newOfficeAttachment;
    }
}
