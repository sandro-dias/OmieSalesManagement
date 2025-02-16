namespace Application.UseCases.DeleteSales
{
    public interface IDeleteSalesUseCase
    {
        Task DeleteSalesAsync(DeleteSalesInput input, CancellationToken cancellationToken);
    }
}
