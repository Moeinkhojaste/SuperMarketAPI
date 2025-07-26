// Validators/CategoryUpdateDtoValidator.cs

using FluentValidation;
using SuperMarketAPI.DTOs;

public class CategoryUpdateDtoValidator : AbstractValidator<CategoryUpdateDto>
{
    public CategoryUpdateDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MinimumLength(2);
    }
}
