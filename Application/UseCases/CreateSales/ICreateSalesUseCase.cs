namespace Application.UseCases.CreateSales
{
    public interface ICreateSalesUseCase
    {
        Task<CreateSalesOutput> CreateSalesAsync(CreateSalesInput input, CancellationToken cancellationToken);
    }
}
