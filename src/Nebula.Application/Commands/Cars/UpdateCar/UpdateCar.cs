using Nebula.Domain.Entities.Cars;

namespace Nebula.Application.Commands.Cars.UpdateCar;

public record UpdateCarCommand : IRequest<Car>
{
    public UpdateCarCommand(long id, string model, DateTime year, string color, string number,
        long registrationNumber, bool isAvailable, long carCategoryId)
    {
        Id = id;
        Model = model;
        Year = year;
        Color = color;
        Number = number;
        RegistrationNumber = registrationNumber;
        IsAvailable = isAvailable;
        CarCategoryId = carCategoryId;
    }

    public long Id { get; set; }
    public string Model { get; set; }
    public DateTime Year { get; set; }
    public string Color { get; set; }
    public string Number { get; set; }
    public long RegistrationNumber { get; set; }
    public bool IsAvailable { get; set; } = true;
    public long CarCategoryId { get; set; }
}

public class UpdateCarCommandHandler : IRequestHandler<UpdateCarCommand, Car>
{
    private readonly IRepository<Car> repository;

    public UpdateCarCommandHandler(IRepository<Car> repository)
    {
        this.repository = repository;
    }

    public async Task<Car> Handle(UpdateCarCommand request, CancellationToken cancellationToken)
    {
        var car = await repository.SelectAsync(x => x.Id.Equals(request.Id))
            ?? throw new NotFoundException("Car was not found!");

        car.Model = request.Model;
        car.Year = request.Year;
        car.Color = request.Color;
        car.Number = request.Number;
        car.RegistrationNumber = request.RegistrationNumber;
        car.IsAvailable = request.IsAvailable;
        car.CarCategoryId = request.CarCategoryId;

        repository.Update(car);
        await repository.SaveAsync();

        return car;
    }
}
