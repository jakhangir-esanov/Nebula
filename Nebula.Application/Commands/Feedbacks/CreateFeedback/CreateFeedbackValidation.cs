namespace Nebula.Application.Commands.Feedbacks.CreateFeedback;

public class CreateFeedbackValidation : AbstractValidator<CreateFeedbackCommand>
{
    public CreateFeedbackValidation()
    {
        RuleFor(x => x.Rating).NotEmpty().NotNull().WithMessage("This field is required!");
        RuleFor(x => x.Comment).NotEmpty().NotNull().WithMessage("This field is required!")
            .MaximumLength(20)
            .MaximumLength(500);

        RuleFor(x => x.FeedbackDate).NotEmpty().NotNull().WithMessage("This field is required!");
        RuleFor(x => x.RentalId).NotEmpty().NotNull().WithMessage("This field is required!");
        RuleFor(x => x.CustomerId).NotEmpty().NotNull().WithMessage("This field is required!");
    }
}
