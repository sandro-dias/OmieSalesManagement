using Domain.Entities;

namespace Application.UseCases.GetSalesById
{
    public class GetSalesByIdOutput
    {
        public GetSalesByIdOutput(Sales? sales, IEnumerable<Product>? products)
        {
            Sales = sales;
            Products = products;
        }
        public GetSalesByIdOutput() {}

        public Sales? Sales { get; set; }
        public IEnumerable<Product>? Products { get; set; }
    }
}
