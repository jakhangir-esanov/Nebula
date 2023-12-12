namespace Nebula.Application.Commands.Cars.CreateCarCategory;

public class CreateCarCategoryValidation : AbstractValidator<CreateCarCategoryCommand> 
{
    public CreateCarCategoryValidation()
    {
        RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("This field is required!");
        RuleFor(x => x.Price).NotNull().NotEmpty().WithMessage("This field is required!");
        RuleFor(x => x.Discount).NotNull().NotEmpty().WithMessage("This field is required!");
    }
}
