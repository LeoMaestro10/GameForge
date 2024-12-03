namespace GameForge.Models
{
    public static class DataStore
    {
        // In-memory store for Wallets
        public static List<Wallet> Wallets { get; set; } = new List<Wallet>
        {
            new Wallet { UserId = 1, Balance = 50 } // Example: User with ID 1 has $50
        };

        // In-memory store for GiftCards
        public static List<GiftCard> GiftCards { get; set; } = new List<GiftCard>
        {
            new GiftCard { Code = "GIFT100", Amount = 100 },
            new GiftCard { Code = "GIFT50", Amount = 50 },
            new GiftCard { Code = "GIFT20", Amount = 20 }
        };

        // In-memory store for Subscription Plans
        public static List<SubscriptionPlan> SubscriptionPlans { get; set; } = new List<SubscriptionPlan>
        {
            new SubscriptionPlan { Id = 1, Name = "Monthly", Price = 10 },
            new SubscriptionPlan { Id = 2, Name = "Yearly", Price = 110 }
        };

        // In-memory store for Coupons
        public static List<Coupon> Coupons { get; set; } = new List<Coupon>
        {
            new Coupon { Code = "FREE10", DiscountAmount = 10 },
            new Coupon { Code = "FREE110", DiscountAmount = 110 }
        };

        // In-memory store for User Subscriptions
        public static List<UserSubscription> UserSubscriptions { get; set; } = new List<UserSubscription>();

        // In-memory store for Games
        public static List<Game> Games { get; set; } = new List<Game>
        {
            new Game { Id = 1, Title = "Game 1", Description = "Awesome game 1", Price = 20 },
            new Game { Id = 2, Title = "Game 2", Description = "Awesome game 2", Price = 30 }
        };
    }
}
