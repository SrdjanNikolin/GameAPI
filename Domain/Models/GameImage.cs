using System.ComponentModel.DataAnnotations.Schema;

namespace GamesApi.Domain.Models
{
    public class GameImage
    {
        public int GameImageId { get; set; }
        public string GameImageData { get; set; }
        [ForeignKey("GameImageForeignKey")]
        public int GameId { get; set; }
        public Game Game { get; set; }
    }
}