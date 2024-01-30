using Nebula.Application.DTOs;
using Nebula.Domain.Entities.Cars;
using Nebula.Domain.Entities.Attachments;
using Nebula.Application.Commands.Attachments.CreateAttachment;

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
    private readonly IMediator mediator;
    private readonly IMapper mapper;
    public UploadCarImageCommandHandler(IRepository<Car> carRepository,
        IMediator mediator, IMapper mapper)
    {
        this.carRepository = carRepository;
        this.mediator = mediator;
        this.mapper = mapper;
    }

    public async Task<CarResultDto> Handle(UploadCarImageCommand request, CancellationToken cancellationToken)
    {
        var car = await this.carRepository.SelectAsync(x => x.Id.Equals(request.Id))
            ?? throw new NotFoundException("Car was not found!");

        var createAttachment = await this.mediator.Send(new CreateCarAttachmentCommand("CarFile", request.Id, request.dto));

        return mapper.Map<CarResultDto>(car);
    }
}
