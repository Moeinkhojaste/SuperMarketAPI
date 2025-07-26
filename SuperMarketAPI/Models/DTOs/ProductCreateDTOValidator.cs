using FluentValidation;
using SuperMarketAPI.Models.DTOs;

namespace SuperMarketAPI.Validators;

public class ProductCreateDtoValidator : AbstractValidator<ProductCreateDto>
{
    public ProductCreateDtoValidator()
    {
        RuleFor(p => p.Name).NotEmpty().MaximumLength(100);
        RuleFor(p => p.Price).GreaterThanOrEqualTo(0);
        RuleFor(p => p.Stock).GreaterThanOrEqualTo(0);
        RuleFor(p => p.CategoryId).GreaterThan(0);
    }
}
