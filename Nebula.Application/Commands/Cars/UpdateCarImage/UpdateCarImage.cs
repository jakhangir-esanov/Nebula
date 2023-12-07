using Nebula.Application.Commands.Attachments.UpdateAttachment;
using Nebula.Application.DTOs;
using Nebula.Domain.Entities.Attachments;
using Nebula.Domain.Entities.Cars;

namespace Nebula.Application.Commands.Cars.UpdateCarImage;

public record UpdateCarImageCommand : IRequest<CarResultDto>
{
    public UpdateCarImageCommand(long carId, long attachmentId, AttachmentCreationDto dto)
    {
        this.carId = carId;
        this.attachmentId = attachmentId;
        this.dto = dto;
    }

    public long carId { get; set; }
    public long attachmentId { get; set; }
    public AttachmentCreationDto dto { get; set; }
}


public class UpdateCarImageCommadHandler : IRequestHandler<UpdateCarImageCommand, CarResultDto>
{
    private readonly IRepository<Car> carRepository;
    private readonly IRepository<CarAttachment> carAttachmentRepository;
    private readonly IMediator mediator;
    private readonly IMapper mapper;
    public UpdateCarImageCommadHandler(IRepository<Car> carRepository, IRepository<CarAttachment> carAttachmentRepository,
        IMediator mediator, IMapper mapper)
    {
        this.carRepository = carRepository;
        this.carAttachmentRepository = carAttachmentRepository;
        this.mediator = mediator;
        this.mapper = mapper;
    }

    public async Task<CarResultDto> Handle(UpdateCarImageCommand request, CancellationToken cancellationToken)
    {
        var car = await this.carRepository.SelectAsync(x => x.Id.Equals(request.carId))
            ?? throw new NotFoundException("You could not update image, because car was not found!");

        var carAttachment = await this.carAttachmentRepository.SelectAsync(x => x.CarId.Equals(request.carId)
            && x.AttamentId.Equals(request.attachmentId))
            ?? throw new NotFoundException("You could not update image, because image was not found!");

        await this.mediator.Send(new UpdateAttachmentCommand("CarFile", request.attachmentId, request.dto));

        return mapper.Map<CarResultDto>(car);
    }
}
