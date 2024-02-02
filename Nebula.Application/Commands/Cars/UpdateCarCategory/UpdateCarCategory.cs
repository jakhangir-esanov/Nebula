using Nebula.Domain.Entities.Cars;

namespace Nebula.Application.Commands.Cars.UpdateCarCategory;

public record UpdateCarCategoryCommand : IRequest<CarCategory>
{
    public UpdateCarCategoryCommand(long id, string name, double fromPrice, 
        double toPrice, string description, double? discount)
    {
        Id = id;
        Name = name;
        FromPrice = fromPrice;
        ToPrice = toPrice;
        Description = description;
        Discount = discount;
    }

    public long Id { get; set; }
    public string Name { get; set; }
    public double FromPrice { get; set; }
    public double ToPrice { get; set; }
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
        carCategory.FromPrice = request.FromPrice;
        carCategory.ToPrice = request.ToPrice;
        carCategory.Description = request.Description;
        carCategory.Discount = request.Discount;

        repository.Update(carCategory);
        await repository.SaveAsync();

        return carCategory;
    }
}
