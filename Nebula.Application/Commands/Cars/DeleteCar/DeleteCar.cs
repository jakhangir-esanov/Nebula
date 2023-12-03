using Nebula.Domain.Entities.Cars;

namespace Nebula.Application.Commands.Cars.DeleteCar;

public record DeleteCarCommand : IRequest<bool>
{
    public DeleteCarCommand(long id)
    {
        Id = id;
    }

    public long Id { get; set; }
}

public class DeleteCarCommandHandler : IRequestHandler<DeleteCarCommand, bool>
{
    private readonly IRepository<Car> repository;

    public DeleteCarCommandHandler(IRepository<Car> repository)
    {
        this.repository = repository;
    }

    public async Task<bool> Handle(DeleteCarCommand request, CancellationToken cancellationToken)
    {
        var car = await repository.SelectAsync(x => x.Id.Equals(request.Id))
            ?? throw new NotFoundException("Car was not found!");

        repository.Drop(car);
        await repository.SaveAsync();

        return true;
    }
}
