namespace GameForge.Models
{
    public class Coupon
    {
        public string Code { get; set; } = string.Empty;  // Coupon code
        public decimal DiscountAmount { get; set; }       // Discount amount
        public bool IsUsed { get; set; } = false;         // Tracks if the coupon is already used
    }
}
