using Application.Data.Repository;

namespace Application.UseCases.GetSales
{
    public class GetSalesUseCase(ISalesRepository salesRepository) : IGetSalesUseCase
    {
        public async Task<GetSalesOutput> GetSalesAsync(CancellationToken cancellationToken = default)
        {
            var sales = await salesRepository.GetPagedResultAsync(pageNumber : 1, pageSize : 10, cancellationToken);
            return new GetSalesOutput(sales.Items.OrderByDescending(x => x.SalesId), sales.TotalCount, sales.PageNumber, sales.PageSize);
        }
    }
}
