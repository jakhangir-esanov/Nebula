namespace Nebula.Application.Commands.People.CreateCustomer;

public class CreateCustomerCommandValidation : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidation()
    {
        RuleFor(x => x.FirstName).NotEmpty().NotNull().WithMessage("This field is required!")
            .MinimumLength(3)
            .MaximumLength(50); 
        
        RuleFor(x => x.LastName).NotEmpty().NotNull().WithMessage("This field is required!")
            .MinimumLength(3)
            .MaximumLength(50);

        RuleFor(x => x.Email).NotEmpty().NotNull().WithMessage("This field is required!")
            .EmailAddress();

        RuleFor(x => x.Phone).NotEmpty().NotNull().WithMessage("This field is required!");

        RuleFor(x => x.Password).NotEmpty().NotNull().WithMessage("This field is required!")
            .MinimumLength(4)
            .MaximumLength(20);

        RuleFor(x => x.DateOfBirth).NotEmpty().NotNull().WithMessage("This field is required!");
        RuleFor(x => x.Address).NotEmpty().NotNull().WithMessage("This field is required!");
        RuleFor(x => x.DrivingLicenseNumber).NotEmpty().NotNull().WithMessage("This field is required!");
        RuleFor(x => x.DrivingLicenseExpirationDate).NotEmpty().NotNull().WithMessage("This field is required!");
    }
}
