namespace _24HCodeChallenge.API.DTOs
{
    public class PizzaSummaryDto
    {
        public string PizzaId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Category { get; set; } = null!;
        public string Ingredients { get; set; } = null!;
        public int QuantitySold { get; set; }
        public decimal TotalSales { get; set; }
    }
}
