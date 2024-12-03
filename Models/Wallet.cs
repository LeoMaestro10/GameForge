namespace GameForge.Models
{
    public class Wallet
    {
        public int UserId { get; set; }  // Foreign key to the User table
        public decimal Balance { get; set; }  // User's current wallet balance
    }
}
