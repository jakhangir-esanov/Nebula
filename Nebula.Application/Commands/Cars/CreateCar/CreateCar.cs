using Nebula.Domain.Entities.Cars;

namespace Nebula.Application.Commands.Cars.CreateCar;

public record CreateCarCommand : IRequest<Car>
{
    public CreateCarCommand(string model, DateTime year, string color, string number, 
        double price, long registrationNumber, bool isAvailable, long carCategoryId)
    {
        Model = model;
        Year = year;
        Color = color;
        Number = number;
        Price = price;
        RegistrationNumber = registrationNumber;
        IsAvailable = isAvailable;
        CarCategoryId = carCategoryId;
    }

    public string Model { get; set; }
    public DateTime Year { get; set; }
    public string Color { get; set; }
    public string Number { get; set; }
    public double Price { get; set; }
    public long RegistrationNumber { get; set; }
    public bool IsAvailable { get; set; } = true;
    public long CarCategoryId { get; set; }
}

public class CreateCarCommandHandler : IRequestHandler<CreateCarCommand, Car>
{
    private readonly IRepository<Car> repository;

    public CreateCarCommandHandler(IRepository<Car> repository)
    {
        this.repository = repository;
    }

    public async Task<Car> Handle(CreateCarCommand request, CancellationToken cancellationToken)
    {
        var car = await repository.SelectAsync(x => x.Number.Equals(request.Number));
        if (car is not null)
            throw new AlreadyExistException("Car is already exist!");

        var newCar = new Car()
        {
            Model = request.Model,
            Year = request.Year,
            Color = request.Color,
            Number = request.Number,
            Price = request.Price,
            RegistrationNumber = request.RegistrationNumber,
            IsAvailable = request.IsAvailable,
            CarCategoryId = request.CarCategoryId
        };

        await repository.InsertAsync(newCar);
        await repository.SaveAsync();

        return newCar;
    }
}
