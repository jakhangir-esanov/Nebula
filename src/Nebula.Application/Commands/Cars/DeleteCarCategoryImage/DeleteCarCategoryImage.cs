
using Nebula.Application.Commands.Attachments.DeleteAttachment;
using Nebula.Domain.Entities.Cars;

namespace Nebula.Application.Commands.Cars.DeleteCarCategoryImage;

public record DeleteCarCategoryImageCommand : IRequest<bool>
{
    public DeleteCarCategoryImageCommand(long id)
    {
        Id = id;
    }

    public long Id { get; set; }
}


public class DeleteCarCategoryImageCommandHandler : IRequestHandler<DeleteCarCategoryImageCommand, bool>
{
    private readonly IRepository<CarCategory> repository;
    private readonly IMediator mediator;
    public DeleteCarCategoryImageCommandHandler(IRepository<CarCategory> repository, IMediator mediator)
    {
        this.repository = repository;
        this.mediator = mediator;
    }

    public async Task<bool> Handle(DeleteCarCategoryImageCommand request, CancellationToken cancellationToken)
    {
        var carCategory = await this.repository.SelectAsync(x => x.Id.Equals(request.Id))
            ?? throw new NotFoundException("CarCategory was not found!");

        var attachmentId = carCategory.AttachmentId
            ?? throw new NotFoundException("CarCategory was not found!");

        await this.mediator.Send(new DeleteAttachmentCommand(attachmentId));
        carCategory.AttachmentId = null;
        carCategory.Attachment = null;

        repository.Update(carCategory);
        await repository.SaveAsync();

        return true;
    }
}
