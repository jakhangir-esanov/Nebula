using Nebula.Domain.Entities.Insurances;

namespace Nebula.Application.Commands.Insurances.UpdateInsuranceCoverage;

public record UpdateInsuranceCoverageCommand : IRequest<InsuranceCoverage>
{
    public UpdateInsuranceCoverageCommand(long id, string name, string description, double cost)
    {
        Id = id;
        Name = name;
        Description = description;
        Cost = cost;
    }

    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Cost { get; set; }
}

public class UpdateInsuranceCoverageCommandHandler : IRequestHandler<UpdateInsuranceCoverageCommand, InsuranceCoverage>
{
    private readonly IRepository<InsuranceCoverage> repository;

    public UpdateInsuranceCoverageCommandHandler(IRepository<InsuranceCoverage> repository)
    {
        this.repository = repository;
    }

    public async Task<InsuranceCoverage> Handle(UpdateInsuranceCoverageCommand request, CancellationToken cancellationToken)
    {
        var insuranceCoverage = await repository.SelectAsync(x => x.Id.Equals(request.Id))
            ?? throw new NotFoundException("InsuranceCoverage was not found!");

        insuranceCoverage.Name = request.Name;
        insuranceCoverage.Description = request.Description;
        insuranceCoverage.Cost = request.Cost;

        repository.Update(insuranceCoverage);
        await repository.SaveAsync();

        return insuranceCoverage;
    }
}
