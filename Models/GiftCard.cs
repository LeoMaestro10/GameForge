namespace GameForge.Models
{
    public class GiftCard
    {
        public required string Code { get; set; }  // Gift card code (unique)
        public decimal Amount { get; set; }  // Amount that the card provides
    }
}
