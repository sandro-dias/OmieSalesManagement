namespace Application.UseCases.CreateSales.Input
{
    public class ProductInput
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal UnitValue { get; set; }
        public decimal TotalValue => Quantity * UnitValue;
    }
}
