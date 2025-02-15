using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Product : EntityBase
    {
        [Key]
        public Guid ProductId { get; set; }
        public long SalesId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal UnitValue { get; set; }
        public decimal TotalValue { get; set; }

        public Product() { }

        public static Product CreateProduct(long salesId, string name, int quantity, decimal unitValue, decimal totalValue)
        {
            return new Product()
            {
                ProductId = Guid.NewGuid(),
                SalesId = salesId,
                Name = name,
                Quantity = quantity,
                UnitValue = unitValue,
                TotalValue = totalValue
            };

        }
    }
}
