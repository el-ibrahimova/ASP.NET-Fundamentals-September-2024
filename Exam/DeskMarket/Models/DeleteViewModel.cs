namespace DeskMarket.Models
{
    public class DeleteViewModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = null!;
        public string AddedOn { get; set; } = null!;
        public string Seller { get; set; } = null!;
        public string SellerId { get; set; } = null!;
    }
}
