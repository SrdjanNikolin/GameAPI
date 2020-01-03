using System.ComponentModel.DataAnnotations;

namespace GamesApi.Domain.Models
{
    public class Game
    {
        public int GameId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public Genre Genre { get; set; }
    }
}