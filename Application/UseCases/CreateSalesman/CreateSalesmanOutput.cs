namespace Application.UseCases.CreateSalesman
{
    public class CreateSalesmanOutput
    {
        public long? SalesmanId { get; set; }
        public string[]? Errors { get; set; }

        public CreateSalesmanOutput(long salesmanId)
        {
            SalesmanId = salesmanId;
        }

        public CreateSalesmanOutput(string[] errors)
        {
            Errors = errors;
        }
    }
}
