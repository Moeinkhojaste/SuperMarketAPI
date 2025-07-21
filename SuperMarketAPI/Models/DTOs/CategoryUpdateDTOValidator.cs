using SuperMarketAPI.Models.DTOs;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace SuperMarketAPI.Models.DTOs
{
    public class CategoryUpdateDTOValidator : AbstractValidator<CategoryUpdateDTO>
    {
        public CategoryUpdateDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Category name cannot be empty!")
                .MinimumLength(2).WithMessage("Category name cannot be less than 2 characters")
                .MaximumLength(20).WithMessage("Category name cannot exceed 20 characters ");
        }

    }
}
