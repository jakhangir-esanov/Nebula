using Nebula.Domain.Entities.Cars;

namespace Nebula.Application.Commands.Cars.DeleteCarCategory;

public record DeleteCarCategoryCommand : IRequest<bool>
{
    public DeleteCarCategoryCommand(long id)
    {
        Id = id;
    }

    public long Id { get; set; }
}


public class DeleteCarCategoryCommandHandler : IRequestHandler<DeleteCarCategoryCommand, bool>
{
    private readonly IRepository<CarCategory> repository;

    public DeleteCarCategoryCommandHandler(IRepository<CarCategory> repository)
    {
        this.repository = repository;
    }

    public async Task<bool> Handle(DeleteCarCategoryCommand request, CancellationToken cancellationToken)
    {
        var carCategory = await repository.SelectAsync(x => x.Id.Equals(request.Id))
            ?? throw new NotFoundException("CarCategory was not found!");

        repository.Drop(carCategory);
        await repository.SaveAsync();

        return true;
    }
}
