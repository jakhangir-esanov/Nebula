using Nebula.Application.Commands.Attachments.CreateAttachment;
using Nebula.Application.DTOs;
using Nebula.Domain.Entities.Cars;

namespace Nebula.Application.Commands.Cars.UploadCarCategoryImage;

public record UploadCarCategoryImageCommand : IRequest<CarCategory>
{
    public UploadCarCategoryImageCommand(long carCategoryId, AttachmentCreationDto dto)
    {
        this.carCategoryId = carCategoryId;
        this.dto = dto;
    }

    public long carCategoryId { get; set; }
    public AttachmentCreationDto dto { get; set; }
}

public class UploadCarCategoryImageCommandHandler : IRequestHandler<UploadCarCategoryImageCommand, CarCategory>
{
    private readonly IRepository<CarCategory> repository;
    private readonly IMediator mediator;
    public UploadCarCategoryImageCommandHandler(IRepository<CarCategory> repository, IMediator mediator)
    {
        this.repository = repository;
        this.mediator = mediator;
    }

    public async Task<CarCategory> Handle(UploadCarCategoryImageCommand request, CancellationToken cancellationToken)
    {
        var carCategry = await this.repository.SelectAsync(x => x.Id.Equals(request.carCategoryId))
            ?? throw new NotFoundException("CarCategory was not found!");

        if (carCategry.AttachmentId is not null)
            throw new AlreadyExistException("Attachment is already exist!");

        var createdAttachment = await this.mediator.Send(new CreateAttachmentCommand("CarCategoryFile", request.dto));
        carCategry.AttachmentId = createdAttachment.Id;
        carCategry.Attachment = createdAttachment;

        this.repository.Update(carCategry);
        await this.repository.SaveAsync();

        return carCategry;
    }
}
