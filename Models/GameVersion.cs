using System;
using System.ComponentModel.DataAnnotations;

namespace GameForge.Models
{
    public class GameVersion
    {
        public int Id { get; set; }

        [Required]
        public int GameId { get; set; } // Foreign key to Game

        [Required]
        [StringLength(15)]
        public required string VersionNumber { get; set; } // e.g., "1.0", "2.1"

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [StringLength(200)]
        public string? Description { get; set; } // Release notes or changelog

        // Navigation property to Game
        public Game? Game { get; set; }
    }
}
