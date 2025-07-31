// Validators/CategoryCreateDtoValidator.cs

using FluentValidation;
using SuperMarketAPI.DTOs;

public class CategoryCreateDtoValidator : AbstractValidator<CategoryCreateDto>
{
    public CategoryCreateDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .NotNull().WithMessage("Name cannot be null.")
            .MinimumLength(2).WithMessage("Name must be at least 2 characters.");
            
    }
}
    