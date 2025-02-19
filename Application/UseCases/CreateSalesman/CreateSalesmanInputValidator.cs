﻿using FluentValidation;

namespace Application.UseCases.CreateSalesman
{
    public class CreateSalesmanInputValidator : AbstractValidator<CreateSalesmanInput>
    {
        public CreateSalesmanInputValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name should not be empty");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password should not be empty");

            RuleFor(x => x.Password)
                .MaximumLength(20)
                .WithMessage("Password length should not be greater than 20 characters");
        }
    }
}
