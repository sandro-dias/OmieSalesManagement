using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Sales : EntityBase
    {
        [Key]
        public long SalesId { get; private set; }
        public DateTime Date { get; private set; }
        public string Customer { get; private set; }
        public decimal Value { get; private set; }

        public Sales() {}

        public static Sales CreateSales(string customer, decimal value)
        {
            return new Sales()
            {
                Date = DateTime.Now,
                Customer = customer, 
                Value = value
            };
        }

        public void UpdateDate(DateTime date) 
        {
            Date = date;
        }

        public void UpdateCustomer(string customer)
        {
            Customer = customer;
        }

        public void UpdateValue(decimal value)
        {
            Value = value;
        }
    }
}
