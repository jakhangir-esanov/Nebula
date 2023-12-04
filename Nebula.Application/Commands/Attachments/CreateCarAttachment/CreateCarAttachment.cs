using Nebula.Domain.Entities.Attachments;
using Nebula.Domain.Entities.Cars;

namespace Nebula.Application.Commands.Attachments.CreateCarAttachment;

public record CreateCarAttachmentCommand : IRequest<CarAttachment>
{
    public CreateCarAttachmentCommand(long carId, long attamentId)
    {
        CarId = carId;
        AttamentId = attamentId;
    }

    public long CarId { get; set; }
    public long AttamentId { get; set; }
}

public class CreateCarAttachmentCommandHandler : IRequestHandler<CreateCarAttachmentCommand, CarAttachment>
{
    private readonly IRepository<CarAttachment> repository;

    public CreateCarAttachmentCommandHandler(IRepository<CarAttachment> repository)
    {
        this.repository = repository;
    }

    public async Task<CarAttachment> Handle(CreateCarAttachmentCommand request, CancellationToken cancellationToken)
    {
        var newCarAttachment = new CarAttachment()
        {
            CarId = request.CarId,
            AttamentId = request.AttamentId
        };

        await repository.InsertAsync(newCarAttachment);
        await repository.SaveAsync();

        return newCarAttachment;
    }
}
