using Nebula.Application.Commands.Attachments.UpdateAttachment;
using Nebula.Application.DTOs;
using Nebula.Domain.Entities.Cars;

namespace Nebula.Application.Commands.Cars.UpdateCarCategoryImage;

public record UpdateCarCategoryImageCommand : IRequest<CarCategory>
{
    public UpdateCarCategoryImageCommand(long carCategoryId, AttachmentCreationDto dto)
    {
        this.carCategoryId = carCategoryId;
        this.dto = dto;
    }

    public long carCategoryId {  get; set; }
    public AttachmentCreationDto dto { get; set; }
}

public class UpdateCarCategoryImageCommandHandler : IRequestHandler<UpdateCarCategoryImageCommand, CarCategory>
{
    private readonly IRepository<CarCategory> repository;
    private readonly IMediator mediator;
    public UpdateCarCategoryImageCommandHandler(IRepository<CarCategory> repository, IMediator mediator)
    {
        this.repository = repository;
        this.mediator = mediator;
    }

    public async Task<CarCategory> Handle(UpdateCarCategoryImageCommand request, CancellationToken cancellationToken)
    {
        var carCategory = await this.repository.SelectAsync(x => x.Id.Equals(request.carCategoryId))
            ?? throw new NotFoundException("CarCategory was not found!");

        var attachmentId = carCategory.AttachmentId
            ?? throw new NotFoundException("Attachment was not found!");

        var updatedAttachment = await this.mediator.Send(new UpdateAttachmentCommand("CarCategoryFile", attachmentId, request.dto));
        carCategory.AttachmentId = updatedAttachment.Id;
        carCategory.Attachment = updatedAttachment;

        this.repository.Update(carCategory);
        await this.repository.SaveAsync();

        return carCategory;
    }
}
