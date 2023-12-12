
using Nebula.Domain.Entities.Insurances;

namespace Nebula.Application.Commands.Insurances.DeleteInsuranceCoverage;

public record DeleteInsuranceCoverageCommand : IRequest<bool>
{
    public DeleteInsuranceCoverageCommand(long id)
    {
        Id = id;
    }

    public long Id { get; set; }
}

public class DeleteInsuranceCoverageCommandHandler : IRequestHandler<DeleteInsuranceCoverageCommand, bool>
{
    private readonly IRepository<InsuranceCoverage> repository;

    public DeleteInsuranceCoverageCommandHandler(IRepository<InsuranceCoverage> repository)
    {
        this.repository = repository;
    }

    public async Task<bool> Handle(DeleteInsuranceCoverageCommand request, CancellationToken cancellationToken)
    {
        var insuranceCoverage = await repository.SelectAsync(x => x.Id.Equals(request.Id))
            ?? throw new NotFoundException("InsuranceCoverage was not found!");

        repository.Drop(insuranceCoverage);
        await repository.SaveAsync();

        return true;
    }
}
