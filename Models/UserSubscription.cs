namespace GameForge.Models
{
    public class UserSubscription
    {
        public int UserId { get; set; }                       // ID of the user
        public int SubscriptionPlanId { get; set; }           // ID of the subscription plan
        public DateTime StartDate { get; set; }               // Subscription start date
        public DateTime EndDate { get; set; }                 // Subscription end date
        public List<int> FreeGameIds { get; set; } = new();   // List of free game IDs
    }
}
