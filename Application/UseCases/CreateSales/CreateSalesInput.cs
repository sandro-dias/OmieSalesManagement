using Application.UseCases.Shared;

namespace Application.UseCases.CreateSales
{
    public class CreateSalesInput
    {
        public string Customer { get; set; }
        public List<ProductInput> Products { get; set; }
        public decimal Value => Products.Sum(x => x.TotalValue);
    }
}
