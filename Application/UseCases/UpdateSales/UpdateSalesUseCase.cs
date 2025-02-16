using Application.Data;
using Application.Data.Specification;
using Application.UseCases.Shared;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.UpdateSales
{
    public class UpdateSalesUseCase(IUnitOfWork unitOfWork, ILogger<UpdateSalesUseCase> logger) : IUpdateSalesUseCase
    {
        public async Task UpdateSalesAsync(UpdateSalesInput input, CancellationToken cancellationToken)
        {
            var sales = await unitOfWork.SalesRepository.GetByIdAsync(input.SalesId, cancellationToken);
            if (sales == null)
            {
                logger.LogError("{[ClassName]} The sales does not exist on database", nameof(UpdateSalesUseCase));
                return;
            }

            sales.UpdateDate(DateTime.Now);
            sales.UpdateCustomer(input.Customer);
            sales.UpdateValue(input.Value);
            await unitOfWork.SalesRepository.UpdateAsync(sales, cancellationToken);

            var products = await unitOfWork.ProductRepository.ListAsync(new GetProductsBySalesId(sales.SalesId), cancellationToken);
            await UpsertProducts(sales.SalesId, products, input.Products, cancellationToken);

            var productsToRemove = products.Where(p => !input.Products.Any(up => up.Name == p.Name));
            if (productsToRemove.Any())
                await unitOfWork.ProductRepository.DeleteListAsync(productsToRemove, cancellationToken);

            await unitOfWork.CommitAsync(cancellationToken);
        }

        private async Task UpsertProducts(long salesId, IEnumerable<Product> products, IEnumerable<ProductInput> inputProducts, CancellationToken cancellationToken)
        {
            foreach (var product in inputProducts)
            {
                var existingProduct = products.FirstOrDefault(x => x.Name == product.Name);
                if (existingProduct is not null)
                {
                    existingProduct.UpdateName(product.Name);
                    existingProduct.UpdateUnitValue(product.UnitValue);
                    existingProduct.UpdateTotalValue(product.TotalValue);
                    existingProduct.UpdateQuantity(product.Quantity);
                    await unitOfWork.ProductRepository.UpdateAsync(existingProduct, cancellationToken);
                }
                else
                {
                    var newProduct = Product.CreateProduct(salesId, product.Name, product.Quantity, product.UnitValue, product.TotalValue);
                    await unitOfWork.ProductRepository.AddAsync(newProduct, cancellationToken);
                }
            }
        }
    }
}
