using Nebula.Domain.Entities.Attachments;

namespace Nebula.Application.Commands.Attachments.UpdateCarAttachment;

public record UpdateCarAttachmentCommand : IRequest<CarAttachment>
{
    public UpdateCarAttachmentCommand(long id, long carId, long attamentId)
    {
        Id = id;
        CarId = carId;
        AttamentId = attamentId;
    }

    public long Id { get; set; }
    public long CarId { get; set; }
    public long AttamentId { get; set; }
}

public class UpdateCarAttachmentCommandHandler : IRequestHandler<UpdateCarAttachmentCommand, CarAttachment>
{
    private readonly IRepository<CarAttachment> repository;

    public UpdateCarAttachmentCommandHandler(IRepository<CarAttachment> repository)
    {
        this.repository = repository;
    }

    public async Task<CarAttachment> Handle(UpdateCarAttachmentCommand request, CancellationToken cancellationToken)
    {
        var carAttachment = await repository.SelectAsync(x => x.Id.Equals(request.Id))
            ?? throw new NotFoundException("CarAttachment was not found!");

        carAttachment.CarId = request.CarId;
        carAttachment.AttamentId = request.AttamentId;

        repository.Update(carAttachment);
        await repository.SaveAsync();

        return carAttachment;
    }
}
