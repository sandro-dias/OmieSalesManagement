namespace Application.UseCases.CreateSales
{
    public class CreateSalesOutput
    {
        public long? SalesId { get; set; }

        public string[]? Errors { get; set; }

        public CreateSalesOutput(long salesId)
        {

            SalesId = salesId;  
        }

        public CreateSalesOutput(string[] errors)
        {
            Errors = errors;
        }
    }
}
