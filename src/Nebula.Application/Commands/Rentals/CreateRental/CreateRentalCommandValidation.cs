namespace Nebula.Application.Commands.Rentals.CreateRental;

public class CreateRentalCommandValidation : AbstractValidator<CreateRentalCommand>
{
    public CreateRentalCommandValidation()
    {
        RuleFor(x => x.CustomerId).NotEmpty().NotNull().WithMessage("This field is required!");
        RuleFor(x => x.CarId).NotEmpty().NotNull().WithMessage("This field is required!");
        RuleFor(x => x.StartDate).NotEmpty().NotNull().WithMessage("This field is required!");
        RuleFor(x => x.EndDate).NotEmpty().NotNull().WithMessage("This field is required!");
    }
}
