using FluentValidation;

namespace Application.UseCases.CreateSales.Validator
{
    public class CreateSalesInputValidator : AbstractValidator<CreateSalesInput>
    {
        public CreateSalesInputValidator()
        {
            RuleFor(x => x.Customer)
                .NotEmpty()
                .WithMessage("Customer should not be empty");
        }
    }
}
