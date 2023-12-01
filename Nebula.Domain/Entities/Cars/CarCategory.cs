using Nebula.Domain.Commons;

namespace Nebula.Domain.Entities.Cars;

public sealed class CarCategory : Auditable
{
    public string Name { get; set; }
    public double Price { get; set; }
    public string Description { get; set; }
    public double? Discount { get; set; }

    public ICollection<Car> Cars { get; set; }
}

