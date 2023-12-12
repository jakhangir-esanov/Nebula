
using Nebula.Domain.Entities.Feedbacks;

namespace Nebula.Application.Commands.Feedbacks.DeleteFeedback;

public record DeleteFeedbackCommand : IRequest<bool>
{
    public DeleteFeedbackCommand(long id)
    {
        Id = id;
    }

    public long Id { get; set; }
}

public class DeleteFeedbackCommandHandler : IRequestHandler<DeleteFeedbackCommand, bool>
{
    private readonly IRepository<Feedback> repository;

    public DeleteFeedbackCommandHandler(IRepository<Feedback> repository)
    {
        this.repository = repository;
    }

    public async Task<bool> Handle(DeleteFeedbackCommand request, CancellationToken cancellationToken)
    {
        var feedback = await repository.SelectAsync(x => x.Id.Equals(request.Id))
            ?? throw new NotFoundException("Feedback was not found!");

        repository.Drop(feedback);
        await repository.SaveAsync();

        return true;
    }
}
