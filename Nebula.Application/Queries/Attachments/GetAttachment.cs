using Nebula.Application.DTOs;
using Nebula.Domain.Entities.Attachments;

namespace Nebula.Application.Queries.Attachments;

public record GetAttachmentQuery : IRequest<AttachmentResultDto>
{
    public long Id { get; set; }
}

public class GetAttachmentQueryHandler : IRequestHandler<GetAttachmentQuery, AttachmentResultDto>
{
    private readonly IRepository<Attachment> repository;
    private readonly IMapper mapper;
    public GetAttachmentQueryHandler(IRepository<Attachment> repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public async Task<AttachmentResultDto> Handle(GetAttachmentQuery request, CancellationToken cancellationToken)
    {
        var attachments = await this.repository.SelectAsync(x => x.Id.Equals(request.Id))
            ?? throw new NotFoundException("Attachment was not found!");

        return mapper.Map<AttachmentResultDto>(attachments);
    }
}
