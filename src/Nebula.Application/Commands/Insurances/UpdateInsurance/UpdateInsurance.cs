using Nebula.Domain.Entities.Insurances;

namespace Nebula.Application.Commands.Insurances.UpdateInsurance;

public record UpdateInsuranceCommand : IRequest<Insurance>
{
    public UpdateInsuranceCommand(long id, string name, long insuranceCoverageId, long customerId)
    {
        Id = id;
        Name = name;
        InsuranceCoverageId = insuranceCoverageId;
        CustomerId = customerId;
    }

    public long Id { get; set; }
    public string Name { get; set; }
    public long InsuranceCoverageId { get; set; }
    public long CustomerId { get; set; }
}

public class UpdateInsuranceCommandHandler : IRequestHandler<UpdateInsuranceCommand, Insurance>
{
    private readonly IRepository<Insurance> repository;

    public UpdateInsuranceCommandHandler(IRepository<Insurance> repository)
    {
        this.repository = repository;
    }

    public async Task<Insurance> Handle(UpdateInsuranceCommand request, CancellationToken cancellationToken)
    {
        var insurance = await repository.SelectAsync(x => x.Id.Equals(request.Id))
            ?? throw new NotFoundException("Insurance was not found!");

        insurance.Name = request.Name;
        insurance.InsuranceCoverageId = request.InsuranceCoverageId;
        insurance.CustomerId = request.CustomerId;

        repository.Update(insurance);
        await repository.SaveAsync();

        return insurance;
    }
}
