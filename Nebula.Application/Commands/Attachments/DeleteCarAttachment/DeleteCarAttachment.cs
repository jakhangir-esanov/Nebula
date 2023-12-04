
using Nebula.Domain.Entities.Attachments;

namespace Nebula.Application.Commands.Attachments.DeleteCarAttachment;

public record DeleteCarAttachmentCommand : IRequest<bool>
{
    public DeleteCarAttachmentCommand(long id)
    {
        Id = id;
    }

    public long Id { get; set; }
}

public class DeleteCarAttachmentCommandHandler : IRequestHandler<DeleteCarAttachmentCommand, bool>
{
    private readonly IRepository<CarAttachment> repository;

    public DeleteCarAttachmentCommandHandler(IRepository<CarAttachment> repository)
    {
        this.repository = repository;
    }

    public async Task<bool> Handle(DeleteCarAttachmentCommand request, CancellationToken cancellationToken)
    {
        var carAttachment = await repository.SelectAsync(x => x.Id.Equals(request.Id))
            ?? throw new NotFoundException("CarAttachment was not found!");

        repository.Drop(carAttachment);
        await repository.SaveAsync();

        return true;
    }
}
