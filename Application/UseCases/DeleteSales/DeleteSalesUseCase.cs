using Application.Data;
using Application.Data.Specification;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.DeleteSales
{
    public class DeleteSalesUseCase : IDeleteSalesUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteSalesUseCase> _logger;

        public DeleteSalesUseCase(IUnitOfWork unitOfWork, ILogger<DeleteSalesUseCase> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task DeleteSalesAsync(DeleteSalesInput input, CancellationToken cancellationToken)
        {
            var sales = await _unitOfWork.SalesRepository.GetByIdAsync(input.SalesId, cancellationToken);
            if (sales == null)
            {
                _logger.LogError("{[ClassName]} The sales does not exist on database", nameof(DeleteSalesUseCase));
                return;
            }

            await _unitOfWork.SalesRepository.DeleteAsync(sales, cancellationToken);

            var products = await _unitOfWork.ProductRepository.ListAsync(new GetProductsBySalesId(sales.SalesId), cancellationToken);
            await _unitOfWork.ProductRepository.DeleteListAsync(products, cancellationToken);

            await _unitOfWork.CommitAsync(cancellationToken);
        }
    }
}
