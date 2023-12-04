using Nebula.Application.DTOs;
using Nebula.Domain.Entities.People;

namespace Nebula.Application.Queries.People.GetUser;

public record RetrieveByIdQuery : IRequest<UserResultDto>
{
    public long Id { get; set; }
}

public class RetrieveByIdQueryHandler : IRequestHandler<RetrieveByIdQuery, UserResultDto>
{
    private readonly IRepository<User> repository;
    private readonly IMapper mapper;
    public RetrieveByIdQueryHandler(IRepository<User> repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public async Task<UserResultDto> Handle(RetrieveByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await repository.SelectAsync(x => x.Id.Equals(request.Id))
            ?? throw new NotFoundException("User was not found!");

        return mapper.Map<UserResultDto>(user);
    }
}
