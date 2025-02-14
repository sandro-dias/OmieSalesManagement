using Application.Data.Repository;
using Domain.Entities;

namespace Infrastructure.Data.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(SalesManagementContext dbContext) : base(dbContext)
        {

        }
    }
}
