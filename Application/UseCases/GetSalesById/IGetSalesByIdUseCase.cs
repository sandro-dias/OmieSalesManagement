namespace Application.UseCases.GetSalesById
{
    public interface IGetSalesByIdUseCase
    {
        Task<GetSalesByIdOutput> GetSalesByIdAsync(GetSalesByIdInput input, CancellationToken cancellationToken);
    }
}
