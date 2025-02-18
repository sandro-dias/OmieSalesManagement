using Ardalis.Specification;
using Domain.Entities;

namespace Application.Data.Specification
{
    public class GetSalesmanByPassword : Specification<Salesman>
    {
        public GetSalesmanByPassword(string name, string password)
        {
            Query
                .Where(c => c.Name == name)
                .Where(c => c.Password == password);
        }
    }
}
