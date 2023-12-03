using Nebula.Domain.Entities.Insurances;

namespace Nebula.Application.Commands.Insurances.CreateInsurance;

public record CreateInsuranceCommand : IRequest<Insurance>
{
    public CreateInsuranceCommand(string name, long insuranceCoverageId, long customerId)
    {
        Name = name;
        InsuranceCoverageId = insuranceCoverageId;
        CustomerId = customerId;
    }

    public string Name { get; set; }
    public long InsuranceCoverageId { get; set; }
    public long CustomerId { get; set; }
}

public class CreateInsuranceCommandHandler : IRequestHandler<CreateInsuranceCommand, Insurance>
{
    private readonly IRepository<Insurance> repository;

    public CreateInsuranceCommandHandler(IRepository<Insurance> repository)
    {
        this.repository = repository;
    }

    public async Task<Insurance> Handle(CreateInsuranceCommand request, CancellationToken cancellationToken)
    {
        var insurance = await repository.SelectAsync(x => x.InsuranceCoverageId.Equals(request.InsuranceCoverageId));
        if (insurance is not null)
            throw new AlreadyExistException("Insurance is already exist!");

        var newInsurance = new Insurance()
        {
            Name = request.Name,
            InsuranceCoverageId = request.InsuranceCoverageId,
            CustomerId = request.CustomerId
        };

        await repository.InsertAsync(newInsurance);
        await repository.SaveAsync();

        return newInsurance;
    }
}
