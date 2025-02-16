namespace Application.UseCases.UpdateSales
{
    public interface IUpdateSalesUseCase
    {
        Task UpdateSalesAsync(UpdateSalesInput input, CancellationToken cancellationToken);
    }
}
