﻿namespace Nebula.Application.Commands.Cars.UpdateCarCategory;

public class UpdateCarCategoryCommandValidation : AbstractValidator<UpdateCarCategoryCommand>
{
    public UpdateCarCategoryCommandValidation()
    {
        RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("This field is required!");
        RuleFor(x => x.Price).NotNull().NotEmpty().WithMessage("This field is required!");
        RuleFor(x => x.Discount).NotNull().NotEmpty().WithMessage("This field is required!");
    }
}
