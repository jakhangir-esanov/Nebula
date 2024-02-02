using Nebula.Domain.Entities.Cars;

namespace Nebula.Application.DTOs;

public class CarCategoryResultDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public double FromPrice { get; set; }
    public double ToPrice { get; set; }
    public string Description { get; set; }
    public double? Discount { get; set; }

    public ICollection<CarResultDto> Cars { get; set; }
}
