using Nebula.Domain.Commons;

namespace Nebula.Domain.Entities.Insurances;

public sealed class InsuranceCoverage : Auditable
{
    public string Name { get; set; }
    public string Description { get; set; }
    public double Cost { get; set; }

    public ICollection<Insurance> Insurances { get; set; }
}
