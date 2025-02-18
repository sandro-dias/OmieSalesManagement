using Application.Data.Repository;

namespace Application.Data
{
    public interface IUnitOfWork
    {
        ISalesRepository SalesRepository { get; }
        IProductRepository ProductRepository { get; }
        ISalesmanRepository SalesmanRepository { get; }
        Task<int> CommitAsync(CancellationToken cancellationToken = default);
    }
}
