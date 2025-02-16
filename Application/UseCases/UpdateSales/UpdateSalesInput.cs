using Application.UseCases.Shared;

namespace Application.UseCases.UpdateSales
{
    public class UpdateSalesInput
    {
        public long SalesId { get; set; }
        public string Customer { get; set; }
        public List<ProductInput> Products { get; set; }
        public decimal Value => Products.Sum(x => x.TotalValue);
    }
}
