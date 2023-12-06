namespace Nebula.Application.Commands.Rentals.UpdateRental;

public class UpdateRentalCommandValidation : AbstractValidator<UpdateRentalCommand>
{
    public UpdateRentalCommandValidation()
    {
        RuleFor(x => x.CustomerId).NotEmpty().NotNull().WithMessage("This field is required!");
        RuleFor(x => x.CarId).NotEmpty().NotNull().WithMessage("This field is required!");
        RuleFor(x => x.StartDate).NotEmpty().NotNull().WithMessage("This field is required!");
        RuleFor(x => x.EndDate).NotEmpty().NotNull().WithMessage("This field is required!");
    }
}
