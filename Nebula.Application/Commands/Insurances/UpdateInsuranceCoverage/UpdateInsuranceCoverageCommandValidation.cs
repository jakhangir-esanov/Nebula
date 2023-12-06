namespace Nebula.Application.Commands.Insurances.UpdateInsuranceCoverage;

public class UpdateInsuranceCoverageCommandValidation : AbstractValidator<UpdateInsuranceCoverageCommand>
{
    public UpdateInsuranceCoverageCommandValidation()
    {
        RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("This field is required!")
            .MinimumLength(3)
            .MaximumLength(50);

        RuleFor(x => x.Description).NotEmpty().NotNull().WithMessage("This field is required!");
        RuleFor(x => x.Cost).NotEmpty().NotNull().WithMessage("This field is required!");
    }
}

