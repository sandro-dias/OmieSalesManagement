namespace Application.UseCases.DeleteSales
{
    public interface IDeleteSalesUseCase
    {
        Task<int> DeleteSalesAsync(DeleteSalesInput input, CancellationToken cancellationToken);
    }
}
