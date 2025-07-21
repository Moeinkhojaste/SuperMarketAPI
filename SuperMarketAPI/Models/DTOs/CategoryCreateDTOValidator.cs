using FluentValidation;
using SuperMarketAPI.Models.DTOs;

namespace SuperMarketAPI.Models.DTOs
    
{
    public class CategoryCreateDTOValidator : AbstractValidator<CategoryCreateDTO>
    {
        public CategoryCreateDTOValidator() 
        { 
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Category name is required")
                .MinimumLength(2).WithMessage("Category name must be at least 2 characters")
                .MaximumLength(50).WithMessage("Category name must not exceed 50 characters");
        }
    }
}
