using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Salesman : EntityBase
    {
        [Key]
        public long SalesmanId { get; set; }
        public string Name { get; private set; }
        public string Password { get; private set; }

        public Salesman() { }

        public static Salesman CreateSalesman(string name, string password)
        {
            return new Salesman()
            {
                Name = name,
                Password = password
            };
        }
    }
}
