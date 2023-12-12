namespace Nebula.Application.Commands.Insurances.CreateInsurance;

public class CreateInsuranceCommandValidation : AbstractValidator<CreateInsuranceCommand>
{
    public CreateInsuranceCommandValidation()
    {
        RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("This field is required!")
            .MinimumLength(3)
            .MaximumLength(100);

        RuleFor(x => x.InsuranceCoverageId).NotEmpty().NotNull().WithMessage("This field is required!");
        RuleFor(x => x.CustomerId).NotEmpty().NotNull().WithMessage("This field is required!");
    }
}
