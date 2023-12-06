using Nebula.Application.DTOs;
using Nebula.Domain.Entities.Attachments;
using Nebula.Domain.Entities.Cars;

namespace Nebula.Application.Queries.Cars.GetCar;

public class GetAllCarAttachmentsQuery : IRequest<IEnumerable<AttachmentResultDto>>
{
    public long Id { get; set; }
}

public class GetAllCarAttachmentsQueryHandler : IRequestHandler<GetAllCarAttachmentsQuery, IEnumerable<AttachmentResultDto>>
{
    private readonly IRepository<Car> carRepository;
    private readonly IRepository<Attachment> attachmentRepository;
    private readonly IRepository<CarAttachment> carAttachmentRepository;
    private readonly IMapper mapper;
    public GetAllCarAttachmentsQueryHandler(IRepository<Car> carRepository, IMapper mapper, IRepository<Attachment> attachmentRepository, IRepository<CarAttachment> carAttachmentRepository)
    {
        this.carRepository = carRepository;
        this.mapper = mapper;
        this.attachmentRepository = attachmentRepository;
        this.carAttachmentRepository = carAttachmentRepository;
    }

    public async Task<IEnumerable<AttachmentResultDto>> Handle(GetAllCarAttachmentsQuery request, CancellationToken cancellationToken)
    {
        var car = await carRepository.SelectAsync(x => x.Id.Equals(request.Id))
            ?? throw new NotFoundException("Car was not found!");

        var carAttachment = carAttachmentRepository.SelectAll().Where(x => x.Id.Equals(car.Id)).ToList();
        var attachment = attachmentRepository.SelectAll().ToList();

        var joinedData = carAttachment
            .Join(
                attachment,
                c => c.AttamentId,
                a => a.Id,
                (carAttach, attach) => attach
            )
            .ToList();

        return mapper.Map<IEnumerable<AttachmentResultDto>>(joinedData);
    }
}
