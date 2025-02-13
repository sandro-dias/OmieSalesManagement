namespace Domain.Entities
{
    public class Product
    {
        public Guid ProductId { get; set; }
        public long SalesId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal UnitValue { get; set; }
        public decimal TotalValue => Quantity * UnitValue;
    }
}
