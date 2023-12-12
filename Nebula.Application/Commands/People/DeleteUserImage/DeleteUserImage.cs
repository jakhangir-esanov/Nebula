
using Nebula.Application.Commands.Attachments.DeleteAttachment;
using Nebula.Domain.Entities.People;

namespace Nebula.Application.Commands.People.DeleteUserImage;

public record DeleteUserImageCommand : IRequest<bool>
{
    public DeleteUserImageCommand(long id)
    {
        Id = id;
    }

    public long Id { get; set; }
}

public class DeleteUserImageCommandHandler : IRequestHandler<DeleteUserImageCommand, bool>
{
    private readonly IRepository<User> repository;
    private readonly IMediator mediator;
    public DeleteUserImageCommandHandler(IRepository<User> repository, IMediator mediator)
    {
        this.repository = repository;
        this.mediator = mediator;
    }

    public async Task<bool> Handle(DeleteUserImageCommand request, CancellationToken cancellationToken)
    {
        var user = await this.repository.SelectAsync(x => x.Id.Equals(request.Id))
            ?? throw new NotFoundException("User was not found!");

        var attachmentId = user.AttachmentId
            ?? throw new NotFoundException("Attachment was not found!");

        await this.mediator.Send(new DeleteAttachmentCommand(attachmentId));
        user.AttachmentId = null;
        user.Attachment = null;

        this.repository.Update(user);
        await this.repository.SaveAsync();

        return true;
    }
}
