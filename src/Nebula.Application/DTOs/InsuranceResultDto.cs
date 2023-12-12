using Nebula.Domain.Entities.Insurances;

namespace Nebula.Application.DTOs;

public class InsuranceResultDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public long InsuranceCoverageId { get; set; }
    public long CustomerId { get; set; }
}
