using Nebula.Application.DTOs;
using Nebula.Domain.Entities.Cars;
using Nebula.Domain.Entities.Attachments;
using Nebula.Application.Commands.Attachments.CreateAttachment;
using Nebula.Application.Commands.Attachments.CreateCarAttachment;

namespace Nebula.Application.Commands.Cars.UploadCarImage;

public record UploadCarImageCommand : IRequest<CarResultDto>
{
    public UploadCarImageCommand(long id, AttachmentCreationDto dto)
    {
        Id = id;
        this.dto = dto;
    }

    public long Id { get; set; }
    public AttachmentCreationDto dto { get; set; }
}

public class UploadCarImageCommandHandler : IRequestHandler<UploadCarImageCommand, CarResultDto>
{
    private readonly IRepository<Car> carRepository;
    private readonly IRepository<CarAttachment> carAttachmentRepository;
    private readonly IMediator mediator;
    private readonly IMapper mapper;
    public UploadCarImageCommandHandler(IRepository<Car> carRepository,
        IRepository<CarAttachment> carAttachmentRepository, IMediator mediator, IMapper mapper)
    {
        this.carRepository = carRepository;
        this.carAttachmentRepository = carAttachmentRepository;
        this.mediator = mediator;
        this.mapper = mapper;
    }

    public async Task<CarResultDto> Handle(UploadCarImageCommand request, CancellationToken cancellationToken)
    {
        var car = await this.carRepository.SelectAsync(x => x.Id.Equals(request.Id))
            ?? throw new NotFoundException("Car was not found!");

        var carAttachment = this.carAttachmentRepository.SelectAll().Where(x => x.CarId.Equals(car.Id)).ToList();
        if (carAttachment.Count() == 10)
            throw new CustomException(429, "Out of limit image");

        var createAttachment = await this.mediator.Send(new CreateAttachmentCommand("CarFile", request.dto));

        await this.mediator.Send(new CreateCarAttachmentCommand(request.Id, createAttachment.Id));
        createAttachment.CarAttachments = carAttachment;
        car.CarAttachments = carAttachment;

        return mapper.Map<CarResultDto>(car);
    }
}
