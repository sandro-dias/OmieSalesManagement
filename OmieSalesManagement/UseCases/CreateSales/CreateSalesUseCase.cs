using Application.Data;
using Application.UseCases.CreateSales.Input;
using Domain.Entities;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.CreateSales
{
    public class CreateSalesUseCase(IValidator<CreateSalesInput> salesValidator, IValidator<ProductInput> productValidator, ILogger<CreateSalesUseCase> logger, IUnitOfWork unitOfWork) : ICreateSalesUseCase
    {
        public async Task<CreateSalesOutput> CreateSalesAsync(CreateSalesInput input, CancellationToken cancellationToken)
        {
            foreach (var product in input.Products)
            {
                var productsValidation = productValidator.Validate(product);
                if (!productsValidation.IsValid)
                    return LogAndReturnError(productsValidation.Errors.Select(x => x.ErrorMessage).ToArray());
            }

            var salesValidation = salesValidator.Validate(input);
            if (!salesValidation.IsValid)
                return LogAndReturnError(salesValidation.Errors.Select(x => x.ErrorMessage).ToArray());

            var sales = Sales.CreateSales(input.Customer, input.Value);
            sales = await unitOfWork.SalesRepository.AddAsync(sales, cancellationToken);

            foreach (var product in input.Products)
            {
                var productList = Product.CreateProduct(sales.SalesId, product.Name, product.Quantity, product.Quantity, product.TotalValue);
                await unitOfWork.ProductRepository.AddAsync(productList, cancellationToken);
            }

            await unitOfWork.CommitAsync(cancellationToken);
            return new CreateSalesOutput(sales.SalesId);
        }

        private CreateSalesOutput LogAndReturnError(string[] errorMessage)
        {
            logger.LogError("[{ClassName}] Creating sales returned an error: {Errors}", nameof(CreateSalesUseCase), errorMessage);
            return new CreateSalesOutput(errorMessage);
        }
    }
}
