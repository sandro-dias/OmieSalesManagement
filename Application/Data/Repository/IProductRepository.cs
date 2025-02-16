using Domain.Entities;

namespace Application.Data.Repository
{
    public interface IProductRepository : IAsyncRepository<Product>
    {
    }
}
