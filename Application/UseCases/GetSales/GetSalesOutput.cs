using Domain.Entities;

namespace Application.UseCases.GetSales
{
    public class GetSalesOutput(IEnumerable<Sales> salesList, int totalCount, int pageNumber, int pageSize)
    {
        public IEnumerable<Sales> SalesList { get; set; } = salesList;
        public int TotalCount { get; set; } = totalCount;
        public int PageNumber { get; set; } = pageNumber;
        public int PageSize { get; set; } = pageSize;
        public decimal TotalSalesValue => SalesList.Sum(x => x.Value);
    }
}
