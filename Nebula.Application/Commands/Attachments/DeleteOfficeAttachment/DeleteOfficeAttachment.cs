
using Nebula.Domain.Entities.Attachments;

namespace Nebula.Application.Commands.Attachments.DeleteOfficeAttachment;

public record DeleteOfficeAttachmentCommand : IRequest<bool>
{
    public DeleteOfficeAttachmentCommand(long id)
    {
        Id = id;
    }

    public long Id { get; set; }
}

public class DeleteOfficeAttachmentCommandHandler : IRequestHandler<DeleteOfficeAttachmentCommand, bool>
{
    private readonly IRepository<OfficeAttachment> repository;

    public DeleteOfficeAttachmentCommandHandler(IRepository<OfficeAttachment> repository)
    {
        this.repository = repository;
    }

    public async Task<bool> Handle(DeleteOfficeAttachmentCommand request, CancellationToken cancellationToken)
    {
        var officeAttachment = await repository.SelectAsync(x => x.Id.Equals(request.Id))
            ?? throw new NotFoundException("OfficeAttachment was not found!");

        repository.Drop(officeAttachment);
        await repository.SaveAsync();

        return true;
    }
}
