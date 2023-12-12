using Nebula.Domain.Entities.Attachments;

namespace Nebula.Application.Commands.Attachments.DeleteAttachment;

public record DeleteAttachmentCommand : IRequest<bool>
{
    public DeleteAttachmentCommand(long id)
    {
        Id = id;
    }

    public long Id { get; set; }
}

public class DeleteAttachmentCommandHandler : IRequestHandler<DeleteAttachmentCommand, bool>
{
    private readonly IRepository<Attachment> repository;

    public DeleteAttachmentCommandHandler(IRepository<Attachment> repository)
    {
        this.repository = repository;
    }

    public async Task<bool> Handle(DeleteAttachmentCommand request, CancellationToken cancellationToken)
    {
        var attachment = await repository.SelectAsync(x => x.Id.Equals(request.Id))
                    ?? throw new NotFoundException("Attachment was not found!");

        File.Delete(attachment.FilePath);

        this.repository.Drop(attachment);
        await this.repository.SaveAsync();

        return true;
    }
}
