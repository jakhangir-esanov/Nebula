using Nebula.Domain.Entities.Cars;

namespace Nebula.Application.Commands.Cars.CreateCarCategory;

public record CreateCarCategoryCommand : IRequest<CarCategory>
{
    public CreateCarCategoryCommand(string name, double fromPrice, 
        double toPrice, string description, double? discount)
    {
        Name = name;
        FromPrice = fromPrice;
        ToPrice = toPrice;
        Description = description;
        Discount = discount;
    }

    public string Name { get; set; }
    public double FromPrice { get; set; }
    public double ToPrice { get; set; }
    public string Description { get; set; }
    public double? Discount { get; set; }
}

public class CreateCarCategoryCommandHandler : IRequestHandler<CreateCarCategoryCommand, CarCategory>
{
    private readonly IRepository<CarCategory> repository;

    public CreateCarCategoryCommandHandler(IRepository<CarCategory> repository)
    {
        this.repository = repository;
    }

    public async Task<CarCategory> Handle(CreateCarCategoryCommand request, CancellationToken cancellationToken)
    {
        var carCategory = await repository.SelectAsync(x => x.Name.Equals(request.Name));
        if (carCategory is not null)
            throw new AlreadyExistException("CarCategory is already exist!");

        var newCarCategory = new CarCategory()
        {
            Name = request.Name,
            FromPrice = request.FromPrice,
            ToPrice = request.ToPrice,
            Description = request.Description,
            Discount = request.Discount
        };

        await repository.InsertAsync(newCarCategory);
        await repository.SaveAsync();

        return newCarCategory;
    }
}


