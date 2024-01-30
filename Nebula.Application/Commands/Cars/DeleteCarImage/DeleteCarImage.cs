using Nebula.Application.Commands.Attachments.DeleteAttachment;
using Nebula.Domain.Entities.Attachments;
using Nebula.Domain.Entities.Cars;

namespace Nebula.Application.Commands.Cars.DeleteCarImage;

public record DeleteCarImageСommand : IRequest<bool>
{
    public DeleteCarImageСommand(long carId, long attachmentId)
    {
        this.carId = carId;
        this.attachmentId = attachmentId;
    }

    public long carId { get; set; }
    public long attachmentId { get; set; }
}

public class DeleteCarImageCommandHandler : IRequestHandler<DeleteCarImageСommand, bool>
{
    private readonly IRepository<Car> carRepository;
    private readonly IMediator mediator;
    public DeleteCarImageCommandHandler(IRepository<Car> carRepository,
        IMediator mediator)
    {
        this.carRepository = carRepository;
        this.mediator = mediator;
    }

    public async Task<bool> Handle(DeleteCarImageСommand request, CancellationToken cancellationToken)
    {
        var car = await this.carRepository.SelectAsync(x => x.Id.Equals(request.carId))
            ?? throw new NotFoundException("Car was not found!");

        await this.mediator.Send(new DeleteAttachmentCommand(request.attachmentId));

        return true;
    }
}
