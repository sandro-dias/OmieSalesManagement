using Application.Data.Repository;
using Domain.Entities;

namespace Infrastructure.Data.Repository
{
    public class SalesmanRepository : Repository<Salesman>, ISalesmanRepository
    {
        public SalesmanRepository(SalesManagementContext dbContext) : base(dbContext)
        {

        }
    }
}
