using Nebula.Application.Commands.Attachments.CreateAttachment;
using Nebula.Application.DTOs;
using Nebula.Domain.Entities.People;

namespace Nebula.Application.Commands.People.UploadUserImage;

public record UploadUserImageCommand : IRequest<User>
{
    public UploadUserImageCommand(long userId, AttachmentCreationDto dto)
    {
        this.userId = userId;
        this.dto = dto;
    }

    public long userId { get; set; }
    public AttachmentCreationDto dto { get; set; }
}

public class UploadUserImageCommandHandler : IRequestHandler<UploadUserImageCommand, User>
{
    private readonly IRepository<User> repository;
    private readonly IMediator mediator;
    public UploadUserImageCommandHandler(IRepository<User> repository, IMediator mediator)
    {
        this.repository = repository;
        this.mediator = mediator;
    }

    public async Task<User> Handle(UploadUserImageCommand request, CancellationToken cancellationToken)
    {
        var user = await this.repository.SelectAsync(x => x.Id.Equals(request.userId))
            ?? throw new NotFoundException("User was not found!");

        if (user.AttachmentId is not null)
            throw new AlreadyExistException("Attachment is already exist!");

        var createdAttachment = await this.mediator.Send(new CreateAttachmentCommand("UserFile", request.dto));
        user.AttachmentId = createdAttachment.Id;
        user.Attachment = createdAttachment;

        this.repository.Update(user);
        await this.repository.SaveAsync();

        return user;
    }
}
