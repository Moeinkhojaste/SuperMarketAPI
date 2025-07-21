using FluentValidation;
using SuperMarketAPI.Models.DTOs;

namespace SuperMarketAPI.Models.DTOs
{
    public class ProductCreateDTOValidator : AbstractValidator<ProductCreateDto>
    {
        public ProductCreateDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MinimumLength(2).WithMessage("Name must be at least 2 characters long.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");
            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero.");
            RuleFor(x => x.Stock)
                .GreaterThanOrEqualTo(0).WithMessage("Stock must be greater than or equal to zero.");
            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("You must assign a category to this product.");
        }
    }

}
