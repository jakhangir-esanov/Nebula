using Nebula.Domain.Entities.Insurances;
using System.Formats.Asn1;

namespace Nebula.Application.Commands.Insurances.CreateInsuranceCoverage;

public record CreateInsuranceCoverageCommand : IRequest<InsuranceCoverage>
{
    public CreateInsuranceCoverageCommand(string name, string description, double cost)
    {
        Name = name;
        Description = description;
        Cost = cost;
    }

    public string Name { get; set; }
    public string Description { get; set; }
    public double Cost { get; set; }
}

public class CreateInsuranceCoverageCommandHandler : IRequestHandler<CreateInsuranceCoverageCommand, InsuranceCoverage>
{
    private readonly IRepository<InsuranceCoverage> repository;

    public CreateInsuranceCoverageCommandHandler(IRepository<InsuranceCoverage> repository)
    {
        this.repository = repository;
    }

    public async Task<InsuranceCoverage> Handle(CreateInsuranceCoverageCommand request, CancellationToken cancellationToken)
    {
        var insuranceCoverage = await repository.SelectAsync(x => x.Name.Equals(request.Name));
        if (insuranceCoverage is not null)
            throw new AlreadyExistException("InsuranceCoverage was not exist!");

        var newInsuranceCoverage = new InsuranceCoverage()
        {
            Name= request.Name,
            Description = request.Description,
            Cost = request.Cost
        };

        await repository.InsertAsync(newInsuranceCoverage);
        await repository.SaveAsync();

        return newInsuranceCoverage;
    }
}
