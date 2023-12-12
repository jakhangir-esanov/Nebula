namespace Nebula.Application.Commands.Cars.CreateCar;

public class CreateCarCommandValidation : AbstractValidator<CreateCarCommand>
{
    public CreateCarCommandValidation()
    {
        RuleFor(x => x.Model).NotEmpty().NotNull().WithMessage("Model field is required!")
            .MinimumLength(3)
            .MaximumLength(200);

        RuleFor(x => x.Year).NotEmpty().NotNull().WithMessage("Year field is required!");
        RuleFor(x => x.Color).NotEmpty().NotNull().WithMessage("This field is required!");
        RuleFor(x => x.Number).NotEmpty().NotNull().WithMessage("This field is required!");
        RuleFor(x => x.RegistrationNumber).NotEmpty().NotNull().WithMessage("This field is required!");
        RuleFor(x => x.CarCategoryId).NotEmpty().NotNull().WithMessage("This field is required!");
    }
}
