using Application.UseCases.CreateSales.Input;
using FluentValidation;

namespace Application.UseCases.CreateSales.Validator
{
    public class ProductInputValidator : AbstractValidator<ProductInput>
    {
        public ProductInputValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name should not be empty");

            RuleFor(x => x.Quantity)
                .NotEmpty()
                .WithMessage("Quantity should not be empty");

            RuleFor(x => x.UnitValue)
                .NotEmpty()
                .WithMessage("UnitValue should not be empty");
        }
    }
}
