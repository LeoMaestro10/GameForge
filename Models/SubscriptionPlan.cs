namespace GameForge.Models
{
    public class SubscriptionPlan
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; // Plan name (e.g., "Monthly", "Yearly")
        public decimal Price { get; set; }              // Price for the subscription
    }
}
