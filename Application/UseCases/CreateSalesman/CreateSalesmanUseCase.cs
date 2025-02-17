using Application.Data;
using Application.Data.Specification;
using Domain.Entities;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.CreateSalesman
{
    public class CreateSalesmanUseCase(IUnitOfWork unitOfWork, ILogger<CreateSalesmanUseCase> logger, IValidator<CreateSalesmanInput> validator) : ICreateSalesmanUseCase
    {
        public async Task<CreateSalesmanOutput> CreateSalesmanAsync(CreateSalesmanInput input, CancellationToken cancellationToken)
        {
            var salesmanValidation = validator.Validate(input);
            if (!salesmanValidation.IsValid)
                return LogAndReturnError(salesmanValidation.Errors.Select(x => x.ErrorMessage).ToArray());

            var salesman = await unitOfWork.SalesmanRepository.FirstOrDefaultAsync(new GetSalesmanByName(input.Name), cancellationToken);
            if (salesman != null)
                return LogAndReturnError(["The salesman already exists on database"]);

            salesman = Salesman.CreateSalesman(input.Name, input.Password);
            salesman = await unitOfWork.SalesmanRepository.AddAsync(salesman, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);
            return new CreateSalesmanOutput(salesman.SalesmanId);
        }

        private CreateSalesmanOutput LogAndReturnError(string[] errorMessage)
        {
            logger.LogError("[{ClassName}] Creating salesman returned an error: {Errors}", nameof(CreateSalesmanUseCase), errorMessage);
            return new CreateSalesmanOutput(errorMessage);
        }
    }
}
