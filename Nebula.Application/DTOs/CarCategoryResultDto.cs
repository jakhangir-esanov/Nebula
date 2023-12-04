namespace Nebula.Application.DTOs;

public class CarCategoryResultDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public string Description { get; set; }
    public double? Discount { get; set; }
}
