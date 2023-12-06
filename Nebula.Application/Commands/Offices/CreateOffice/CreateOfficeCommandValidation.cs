namespace Nebula.Application.Commands.Offices.CreateOffice;

public class CreateOfficeCommandValidation : AbstractValidator<CreateOfficeCommand>
{
    public CreateOfficeCommandValidation()
    {
        RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("This field is required!")
            .MinimumLength(2)
            .MaximumLength(50);
        RuleFor(x => x.Address).NotEmpty().NotNull().WithMessage("This field is required!")
            .MaximumLength(2);
        RuleFor(x => x.City).NotEmpty().NotNull().WithMessage("This field is required!")
            .MaximumLength(2);
        RuleFor(x => x.State).NotEmpty().NotNull().WithMessage("This field is required!")
            .MinimumLength(2);
        RuleFor(x => x.Country).NotEmpty().NotNull().WithMessage("This field is required!")
            .MinimumLength(2);
        RuleFor(x => x.PostalCode).NotEmpty().NotNull().WithMessage("This field is required!");
        RuleFor(x => x.Phone).NotEmpty().NotNull().WithMessage("This field is required!");
        RuleFor(x => x.Email).NotEmpty().NotNull().WithMessage("This field is required!")
            .EmailAddress();
        RuleFor(x => x.Description).NotEmpty().NotNull().WithMessage("This field is required!")
            .MinimumLength(2)
            .MaximumLength(500);
    }
}
