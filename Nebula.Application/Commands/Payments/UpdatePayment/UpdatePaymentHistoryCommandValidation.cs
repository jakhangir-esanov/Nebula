namespace Nebula.Application.Commands.Payments.UpdatePayment;

public class UpdatePaymentHistoryCommandValidation : AbstractValidator<UpdatePaymentCommand>
{
    public UpdatePaymentHistoryCommandValidation()
    {
        RuleFor(x => x.Amount).NotEmpty().NotNull().WithMessage("This field is required!");
        RuleFor(x => x.PaymentType).NotEmpty().NotNull().WithMessage("This field is required!");
        RuleFor(x => x.CustomerId).NotEmpty().NotNull().WithMessage("This field is required!");
        RuleFor(x => x.RentalId).NotEmpty().NotNull().WithMessage("This field is required!");
        RuleFor(x => x.PaymentStatus).NotEmpty().NotNull().WithMessage("This field is required!");
    }
}
