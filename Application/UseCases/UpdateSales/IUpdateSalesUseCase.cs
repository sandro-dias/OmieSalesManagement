namespace Application.UseCases.UpdateSales
{
    public interface IUpdateSalesUseCase
    {
        Task<int> UpdateSalesAsync(UpdateSalesInput input, CancellationToken cancellationToken);
    }
}
