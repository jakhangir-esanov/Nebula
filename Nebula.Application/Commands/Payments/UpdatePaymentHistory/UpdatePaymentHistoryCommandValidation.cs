namespace Nebula.Application.Commands.Payments.UpdatePaymentHistory;

public class UpdatePaymentHistoryCommandValidation : AbstractValidator<UpdatePaymentHistoryCommand>
{
    public UpdatePaymentHistoryCommandValidation()
    {
        RuleFor(x => x.Date).NotEmpty().NotNull().WithMessage("This field is required!");
        RuleFor(x => x.Amount).NotEmpty().NotNull().WithMessage("This field is required!");
        RuleFor(x => x.PaymentType).NotEmpty().NotNull().WithMessage("This field is required!");
        RuleFor(x => x.PaymentId).NotEmpty().NotNull().WithMessage("This field is required!");
        RuleFor(x => x.CustomerId).NotEmpty().NotNull().WithMessage("This field is required!");
        RuleFor(x => x.RentalId).NotEmpty().NotNull().WithMessage("This field is required!");
    }
}
