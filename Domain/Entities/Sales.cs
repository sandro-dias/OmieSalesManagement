using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Sales
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long SalesId { get; set; }
        public DateTime Date { get; set; }
        public string Customer { get; set; }
        public decimal Value { get; set; }
        public List<Product> ProductList { get; set; }
    }
}
