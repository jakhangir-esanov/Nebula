namespace Nebula.Application.Commands.Payments.CreatePayment;

public class CreatePaymentCommandValidation : AbstractValidator<CreatePaymentCommand>
{
    public CreatePaymentCommandValidation()
    {
        RuleFor(x => x.Amount).NotEmpty().NotNull().WithMessage("This field is required!");
        RuleFor(x => x.PaymentType).NotEmpty().NotNull().WithMessage("This field is required!");
        RuleFor(x => x.CustomerId).NotEmpty().NotNull().WithMessage("This field is required!");
        RuleFor(x => x.RentalId).NotEmpty().NotNull().WithMessage("This field is required!");
        RuleFor(x => x.PaymentStatus).NotEmpty().NotNull().WithMessage("This field is required!");
    }
}
