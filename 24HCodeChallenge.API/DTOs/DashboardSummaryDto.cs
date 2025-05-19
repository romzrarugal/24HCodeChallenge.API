namespace _24HCodeChallenge.API.DTOs
{
    public class DashboardSummaryDto
    {
        public decimal TotalSales { get; set; }
        public int TotalQuantitySold { get; set; }
        public int TotalOrders { get; set; }
        public List<BestSellerDto> BestSellers { get; set; } = new();
    }
    public class BestSellerDto
    {
        public string Category { get; set; } = string.Empty;
        public string PizzaName { get; set; } = string.Empty;
        public int QuantitySold { get; set; }
        public decimal TotalSales { get; set; }
    }
}
