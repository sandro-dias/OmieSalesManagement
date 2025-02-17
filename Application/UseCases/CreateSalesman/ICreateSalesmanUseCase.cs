namespace Application.UseCases.CreateSalesman
{
    public interface ICreateSalesmanUseCase
    {
        Task<CreateSalesmanOutput> CreateSalesmanAsync(CreateSalesmanInput input, CancellationToken cancellationToken);
    }
}
