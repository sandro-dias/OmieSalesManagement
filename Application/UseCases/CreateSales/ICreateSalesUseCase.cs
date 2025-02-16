using Application.UseCases.CreateSales.Input;

namespace Application.UseCases.CreateSales
{
    public interface ICreateSalesUseCase
    {
        Task<CreateSalesOutput> CreateSalesAsync(CreateSalesInput input, CancellationToken cancellationToken);
    }
}
