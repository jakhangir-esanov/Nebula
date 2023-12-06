namespace Nebula.Application.Commands.People.CreateUser;

public class CreateUserCommandValidation : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidation()
    {
        RuleFor(x => x.FirstName).NotEmpty().NotNull().WithMessage("This field is required!")
            .MinimumLength(3)
            .MaximumLength(50);

        RuleFor(x => x.LastName).NotEmpty().NotNull().WithMessage("This field is required!")
            .MinimumLength(3)
            .MaximumLength(50);

        RuleFor(x => x.username).NotEmpty().NotNull().WithMessage("This field is required!");

        RuleFor(x => x.Email).NotEmpty().NotNull().WithMessage("This field is required!")
            .EmailAddress();

        RuleFor(x => x.Phone).NotEmpty().NotNull().WithMessage("This field is required!");

        RuleFor(x => x.Password).NotEmpty().NotNull().WithMessage("This field is required!")
            .MinimumLength(4)
            .MaximumLength(20);

        RuleFor(x => x.DateOfBirth).NotEmpty().NotNull().WithMessage("This field is required!");
        RuleFor(x => x.UserRole).NotEmpty().NotNull().WithMessage("This field is required!");
        RuleFor(x => x.OfficeId).NotEmpty().NotNull().WithMessage("This field is required!");
    }
}
