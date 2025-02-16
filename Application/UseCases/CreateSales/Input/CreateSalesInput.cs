namespace Application.UseCases.CreateSales.Input
{
    public class CreateSalesInput
    {
        public string Customer { get; set; }
        public List<ProductInput> Products { get; set; }
        public decimal Value => Products.Sum(x => x.TotalValue);
    }
}
