using Nebula.Application.Commands.Attachments.UpdateAttachment;
using Nebula.Application.DTOs;
using Nebula.Domain.Entities.People;

namespace Nebula.Application.Commands.People.UpdateUserImage;

public record UpdateUserImageCommand : IRequest<User>
{
    public UpdateUserImageCommand(long userId, AttachmentCreationDto dto)
    {
        this.userId = userId;
        this.dto = dto;
    }

    public long userId { get; set; }
    public AttachmentCreationDto dto { get; set; }
}

public class UpdateUserImageCommandHandler : IRequestHandler<UpdateUserImageCommand, User>
{
    private readonly IRepository<User> repository;
    private readonly IMediator mediator;
    public UpdateUserImageCommandHandler(IRepository<User> repository, IMediator mediator)
    {
        this.repository = repository;
        this.mediator = mediator;
    }

    public async Task<User> Handle(UpdateUserImageCommand request, CancellationToken cancellationToken)
    {
        var user = await this.repository.SelectAsync(x => x.Id.Equals(request.userId))
            ?? throw new NotFoundException("User was not found!");

        var userAttachmentId = user.AttachmentId
            ?? throw new NotFoundException("Attachment was not found!");

        var updatedAttachment = await this.mediator.Send(new UpdateAttachmentCommand("UserFile", userAttachmentId, request.dto));
        user.AttachmentId = updatedAttachment.Id;
        user.Attachment = updatedAttachment;

        this.repository.Update(user);
        await this.repository.SaveAsync();

        return user;
    }
}
