﻿using Nebula.Domain.Entities.Insurances;

namespace Nebula.Application.DTOs;

public class InsuranceCoverageResultDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Cost { get; set; }

    public ICollection<InsuranceResultDto> Insurances { get; set; }

}
