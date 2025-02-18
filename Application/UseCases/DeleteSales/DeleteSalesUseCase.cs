using Application.Data;
using Application.Data.Specification;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.DeleteSales
{
    public class DeleteSalesUseCase(IUnitOfWork unitOfWork, ILogger<DeleteSalesUseCase> logger) : IDeleteSalesUseCase
    {
        public async Task<int> DeleteSalesAsync(DeleteSalesInput input, CancellationToken cancellationToken)
        {
            var sales = await unitOfWork.SalesRepository.GetByIdAsync(input.SalesId, cancellationToken);
            if (sales == null)
            {
                logger.LogError("{[ClassName]} The sales does not exist on database", nameof(DeleteSalesUseCase));
                return 0;
            }

            await unitOfWork.SalesRepository.DeleteAsync(sales, cancellationToken);

            var products = await unitOfWork.ProductRepository.ListAsync(new GetProductsBySalesId(sales.SalesId), cancellationToken);
            await unitOfWork.ProductRepository.DeleteListAsync(products, cancellationToken);

            return await unitOfWork.CommitAsync(cancellationToken);
        }
    }
}
