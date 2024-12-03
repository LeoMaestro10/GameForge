namespace GameForge.Models
{
    public class Group
    {
        public string Name { get; set; } = string.Empty;
        public int UserId { get; set; } // Ensures groups are tied to the user
        public List<Game> Games { get; set; } = new();

    }
}
