using Nebula.Domain.Entities.Feedbacks;
using Nebula.Domain.Enums;

namespace Nebula.Application.Commands.Feedbacks.CreateFeedback;

public record CreateFeedbackCommand : IRequest<Feedback>
{
    public CreateFeedbackCommand(Rating rating, string comment, DateTime feedbackDate, long rentalId, long customerId)
    {
        Rating = rating;
        Comment = comment;
        FeedbackDate = feedbackDate;
        RentalId = rentalId;
        CustomerId = customerId;
    }

    public Rating Rating { get; set; }
    public string Comment { get; set; }
    public DateTime FeedbackDate { get; set; }
    public long RentalId { get; set; }
    public long CustomerId { get; set; }
}

public class CreateFeedbackCommandHandler : IRequestHandler<CreateFeedbackCommand, Feedback>
{
    private readonly IRepository<Feedback> repository;

    public CreateFeedbackCommandHandler(IRepository<Feedback> repository)
    {
        this.repository = repository;
    }

    public async Task<Feedback> Handle(CreateFeedbackCommand request, CancellationToken cancellationToken)
    {
        var feedback = await repository.SelectAsync(x => x.RentalId.Equals(request.RentalId));
        if (feedback is not null)
            throw new AlreadyExistException("Feedback is already exist!");

        var newFeedback = new Feedback()
        {
            Rating = request.Rating,
            Comment = request.Comment,
            FeedbackDate = request.FeedbackDate,
            RentalId = request.RentalId,
            CustomerId = request.CustomerId
        };

        await repository.InsertAsync(newFeedback);
        await repository.SaveAsync();

        return newFeedback;
    }
}
