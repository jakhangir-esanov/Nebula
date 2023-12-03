using Nebula.Domain.Entities.Cars;

namespace Nebula.Application.Commands.Cars.UpdateCarCategory;

public record UpdateCarCategoryCommand : IRequest<CarCategory>
{
    public UpdateCarCategoryCommand(long id, string name, double price, string description, double? discount)
    {
        Id = id;
        Name = name;
        Price = price;
        Description = description;
        Discount = discount;
    }

    public long Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public string Description { get; set; }
    public double? Discount { get; set; }
}

public class UpdateCarCategoryCommandHandler : IRequestHandler<UpdateCarCategoryCommand, CarCategory>
{
    private readonly IRepository<CarCategory> repository;

    public UpdateCarCategoryCommandHandler(IRepository<CarCategory> repository)
    {
        this.repository = repository;
    }

    public async Task<CarCategory> Handle(UpdateCarCategoryCommand request, CancellationToken cancellationToken)
    {
        var carCategory = await repository.SelectAsync(x => x.Id.Equals(request.Id))
            ?? throw new NotFoundException("CarCategory was not found!");

        carCategory.Name = request.Name;
        carCategory.Price = request.Price;
        carCategory.Description = request.Description;
        carCategory.Discount = request.Discount;

        repository.Update(carCategory);
        await repository.SaveAsync();

        return carCategory;
    }
}
