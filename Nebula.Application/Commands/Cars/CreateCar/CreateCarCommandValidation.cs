namespace Nebula.Application.Commands.Cars.CreateCar;

public class CreateCarCommandValidation : AbstractValidator<CreateCarCommand>
{
    public CreateCarCommandValidation()
    {
        RuleFor(x => x.Model).NotEmpty().NotNull().WithMessage("Model field is required!")
            .MinimumLength(3)
            .MaximumLength(200);
    }
}
