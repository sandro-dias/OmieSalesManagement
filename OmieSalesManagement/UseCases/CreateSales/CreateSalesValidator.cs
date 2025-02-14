using FluentValidation;

namespace Application.UseCases.CreateSales
{
    public class CreateSalesValidator : AbstractValidator<CreateSalesInput>
    {
        public CreateSalesValidator()
        {
            RuleFor(x => x.Customer)
                .NotEmpty()
                .WithMessage("Customer should not be empty");

            //TODO: replicar para demais propriedades
        }
    }
}
