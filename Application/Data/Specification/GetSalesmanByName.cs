using Ardalis.Specification;
using Domain.Entities;

namespace Application.Data.Specification
{
    public class GetSalesmanByName : Specification<Salesman>
    {
        public GetSalesmanByName(string name)
        {
            Query
                .Where(c => c.Name == name);
        }
    }
}
