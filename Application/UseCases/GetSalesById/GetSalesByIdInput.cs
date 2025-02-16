namespace Application.UseCases.GetSalesById
{
    public class GetSalesByIdInput(long salesId)
    {
        public long SalesId { get; set; } = salesId;
    }
}
