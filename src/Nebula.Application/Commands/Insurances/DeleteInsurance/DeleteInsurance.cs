using Nebula.Domain.Entities.Insurances;

namespace Nebula.Application.Commands.Insurances.DeleteInsurance;

public record DeleteInsuranceCommand : IRequest<bool>
{
    public DeleteInsuranceCommand(long id)
    {
        Id = id;
    }

    public long Id { get; set; }    
}

public class DeleteInsuranceCommandHandler : IRequestHandler<DeleteInsuranceCommand, bool>
{
    private readonly IRepository<Insurance> repository;

    public DeleteInsuranceCommandHandler(IRepository<Insurance> repository)
    {
        this.repository = repository;
    }

    public async Task<bool> Handle(DeleteInsuranceCommand request, CancellationToken cancellationToken)
    {
        var insurance = await repository.SelectAsync(x => x.Id.Equals(request.Id))
            ?? throw new NotFoundException("Insurance was not found!");

        repository.Drop(insurance);
        await repository.SaveAsync();

        return true;
    }
}
