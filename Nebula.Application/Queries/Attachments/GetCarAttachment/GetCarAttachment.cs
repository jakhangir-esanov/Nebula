using Nebula.Application.DTOs;
using Nebula.Domain.Entities.Attachments;

namespace Nebula.Application.Queries.Attachments.GetCarAttachment;

public record GetCarAttachmentQuery : IRequest<CarAttachmentResultDto>
{
    public long Id { get; set; }
}

public class GetCarAttachmentQueryHandler : IRequestHandler<GetCarAttachmentQuery, CarAttachmentResultDto>
{
    private readonly IRepository<CarAttachment> repository;
    private readonly IMapper mapper;

    public GetCarAttachmentQueryHandler(IRepository<CarAttachment> repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public async Task<CarAttachmentResultDto> Handle(GetCarAttachmentQuery request, CancellationToken cancellationToken)
    {
        var carAttachment = await repository.SelectAsync(x => x.Id.Equals(request.Id))
            ?? throw new NotFoundException("CarAttachment was not found!");

        return mapper.Map<CarAttachmentResultDto>(carAttachment);
    }
}
