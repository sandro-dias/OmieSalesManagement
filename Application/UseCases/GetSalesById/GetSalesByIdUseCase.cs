using Application.Data;
using Application.Data.Specification;
using Application.UseCases.DeleteSales;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.GetSalesById
{
    internal class GetSalesByIdUseCase(ILogger<GetSalesByIdUseCase> logger, IUnitOfWork unitOfWork) : IGetSalesByIdUseCase
    {
        public async Task<GetSalesByIdOutput> GetSalesByIdAsync(GetSalesByIdInput input, CancellationToken cancellationToken)
        {
            var sales = await unitOfWork.SalesRepository.GetByIdAsync(input.SalesId, cancellationToken);
            if (sales == null)
            {
                logger.LogError("{[ClassName]} The sales does not exist on database", nameof(DeleteSalesUseCase));
                return new GetSalesByIdOutput();
            }

            var products = await unitOfWork.ProductRepository.ListAsync(new GetProductsBySalesId(sales.SalesId), cancellationToken);
            return new GetSalesByIdOutput(sales, products);
        }
    }
}
