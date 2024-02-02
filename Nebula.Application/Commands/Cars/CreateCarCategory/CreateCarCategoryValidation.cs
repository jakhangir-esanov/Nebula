namespace Nebula.Application.Commands.Cars.CreateCarCategory;

public class CreateCarCategoryValidation : AbstractValidator<CreateCarCategoryCommand> 
{
    public CreateCarCategoryValidation()
    {
        RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("This field is required!");
        RuleFor(x => x.FromPrice).NotNull().NotEmpty().WithMessage("This field is required!");
        RuleFor(x => x.ToPrice).NotNull().NotEmpty().WithMessage("This field is required!");
        RuleFor(x => x.Discount).NotNull().NotEmpty().WithMessage("This field is required!");
    }
}
