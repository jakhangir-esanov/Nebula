
using Nebula.Domain.Entities.Offices;

namespace Nebula.Application.Commands.Offices.DeleteOffice;

public record DeleteOfficeCommand : IRequest<bool>
{
    public DeleteOfficeCommand(long id)
    {
        Id = id;
    }

    public long Id { get; set; }
}

public class DeleteOfficeCommandHandler : IRequestHandler<DeleteOfficeCommand, bool>
{
    private readonly IRepository<Office> repository;

    public DeleteOfficeCommandHandler(IRepository<Office> repository)
    {
        this.repository = repository;
    }

    public async Task<bool> Handle(DeleteOfficeCommand request, CancellationToken cancellationToken)
    {
        var office = await repository.SelectAsync(x => x.Id.Equals(request.Id))
            ?? throw new NotFoundException("Office was not found!");

        repository.Drop(office);
        await repository.SaveAsync();

        return true;
    }
}
