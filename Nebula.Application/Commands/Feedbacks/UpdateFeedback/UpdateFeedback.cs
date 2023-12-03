using Nebula.Domain.Entities.Feedbacks;
using Nebula.Domain.Enums;

namespace Nebula.Application.Commands.Feedbacks.UpdateFeedback;

public record UpdateFeedbackCommand : IRequest<Feedback>
{
    public UpdateFeedbackCommand(long id, Rating rating, string comment, DateTime feedbackDate,
        long rentalId, long customerId)
    {
        Id = id;
        Rating = rating;
        Comment = comment;
        FeedbackDate = feedbackDate;
        RentalId = rentalId;
        CustomerId = customerId;
    }

    public long Id { get; set; }
    public Rating Rating { get; set; }
    public string Comment { get; set; }
    public DateTime FeedbackDate { get; set; }
    public long RentalId { get; set; }
    public long CustomerId { get; set; }
}

public class UpdateFeedbackCommandHandler : IRequestHandler<UpdateFeedbackCommand, Feedback>
{
    private readonly IRepository<Feedback> repository;

    public UpdateFeedbackCommandHandler(IRepository<Feedback> repository)
    {
        this.repository = repository;
    }

    public async Task<Feedback> Handle(UpdateFeedbackCommand request, CancellationToken cancellationToken)
    {
        var feedback = await repository.SelectAsync(x => x.Id.Equals(request.Id))
            ?? throw new NotFoundException("Feedback was not found!");

        feedback.Rating = request.Rating;
        feedback.Comment = request.Comment;
        feedback.FeedbackDate = request.FeedbackDate;
        feedback.RentalId = request.RentalId;
        feedback.CustomerId = request.CustomerId;

        repository.Update(feedback);
        await repository.SaveAsync();

        return feedback;
    }
}
