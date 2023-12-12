using Nebula.Domain.Commons;
using Nebula.Domain.Entities.People;

namespace Nebula.Domain.Entities.Insurances;

public sealed class Insurance : Auditable
{
    public string Name { get; set; }

    public long InsuranceCoverageId { get; set; }
    public InsuranceCoverage InsuranceCoverage { get; set; }

    public long CustomerId { get; set; }
    public Customer Customer { get; set; }
}
