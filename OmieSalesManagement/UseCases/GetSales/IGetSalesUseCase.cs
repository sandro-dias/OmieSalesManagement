using Domain.Entities;

namespace Application.UseCases.GetSales
{
    public interface IGetSalesUseCase
    {
        Task<GetSalesOutput> GetSalesAsync(CancellationToken cancellationToken = default);
    }
}
