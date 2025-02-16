using Ardalis.Specification;
using Domain.Entities;

namespace Application.Data.Specification
{
    public class GetProductsBySalesId : Specification<Product>
    {
        public GetProductsBySalesId(long salesId)
        {
            Query
                .Where(c => c.SalesId == salesId);
        }
    }
}
