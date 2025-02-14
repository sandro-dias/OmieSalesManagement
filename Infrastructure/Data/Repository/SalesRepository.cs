using Application.Data.Repository;
using Domain.Entities;

namespace Infrastructure.Data.Repository
{
    public class SalesRepository : Repository<Sales>, ISalesRepository
    {
        public SalesRepository(SalesManagementContext dbContext) : base(dbContext)
        {
                
        }
    }
}
