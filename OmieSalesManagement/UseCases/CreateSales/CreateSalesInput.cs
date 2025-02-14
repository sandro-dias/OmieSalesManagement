using Domain.Entities;

namespace Application.UseCases.CreateSales
{
    public class CreateSalesInput
    {
        public DateTime Date { get; set; }
        public string Customer { get; set; }
        public decimal Value { get; set; }
        public List<Product> ProductList { get; set; }
    }
}
