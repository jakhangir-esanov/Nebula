namespace Nebula.Application.Commands.Insurances.UpdateInsurance;

public class UpdateInsuranceCommandValidation : AbstractValidator<UpdateInsuranceCommand>
{
    public UpdateInsuranceCommandValidation()
    {
        RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("This field is required!")
            .MinimumLength(3)
            .MaximumLength(100);

        RuleFor(x => x.InsuranceCoverageId).NotEmpty().NotNull().WithMessage("This field is required!");
        RuleFor(x => x.CustomerId).NotEmpty().NotNull().WithMessage("This field is required!");
    }
}
