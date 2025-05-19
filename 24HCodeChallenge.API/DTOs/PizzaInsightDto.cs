namespace _24HCodeChallenge.API.DTOs
{
    public class PizzaInsightDto
    {
        public Dictionary<string, decimal> SalesPerSize { get; set; } = new();
        public Dictionary<string, decimal> MonthlySales { get; set; } = new();
        public Dictionary<string, int> QuantityPerMonth { get; set; } = new();
    }
}
