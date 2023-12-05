using Nebula.Application.DTOs;
using Nebula.Domain.Entities.Attachments;

namespace Nebula.Application.Queries.Cars.GetCar;

public class GetAllCarAttachmentsQuery : IRequest<IEnumerable<AttachmentResultDto>>
{
    public long Id { get; set; }
}

public class GetAllCarAttachmentsQueryHandler : IRequestHandler<GetAllCarAttachmentsQuery, IEnumerable<AttachmentResultDto>>
{
    private readonly IRepository<CarAttachment> repository;
    private readonly IRepository<Attachment> attachmentRepository;
    private readonly IRepository<CarAttachment> carAttachmentRepository;
    private readonly IMapper mapper;
    public GetAllCarAttachmentsQueryHandler(IRepository<CarAttachment> repository, IMapper mapper, IRepository<Attachment> attachmentRepository, IRepository<CarAttachment> carAttachmentRepository)
    {
        this.repository = repository;
        this.mapper = mapper;
        this.attachmentRepository = attachmentRepository;
        this.carAttachmentRepository = carAttachmentRepository;
    }

    public async Task<IEnumerable<AttachmentResultDto>> Handle(GetAllCarAttachmentsQuery request, CancellationToken cancellationToken)
    {
        var carAttachment = carAttachmentRepository.SelectAll().Where(x => x.Id.Equals(request.Id)).ToList();
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
