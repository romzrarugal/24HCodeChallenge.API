namespace _24HCodeChallenge.API.DTOs
{
    public class PizzaChartDataDto
    {
        public string Category { get; set; } = string.Empty;
        public List<PizzaChartItemDto> Pizzas { get; set; } = new();
    }

    public class PizzaChartItemDto
    {
        public string PizzaName { get; set; } = string.Empty;
        public int QuantitySold { get; set; }
        public decimal TotalSales { get; set; }
    }
}
