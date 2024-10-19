namespace DeskMarket.Models
{
    public class IndexViewModel
    {
        public string ProductName { get; set; } = null!;
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public int Id { get; set; }
        public bool IsSeller { get; set; }
        public bool HasBought { get; set; }
    }
}
