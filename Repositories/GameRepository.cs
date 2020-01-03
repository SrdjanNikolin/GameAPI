using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamesApi.Domain.Models;
using GamesApi.Domain.Repositores;

namespace GamesApi.Repositories
{
    public class GameRepository : IGameRepository
    {
        public List<Game> Games { get; set; }
        public GameRepository()
        {
            Games = new List<Game>() 
            { 
                new Game() { GameId = 1, Name = "Assassing Creed", Genre = Genre.Adventure, Price = 59.99 },
                new Game() { GameId = 2, Name = "Forza 4", Genre = Genre.Racing, Price = 49.99 },
                new Game() { GameId = 3, Name = "Battlefield 5", Genre = Genre.FPS, Price = 39.99 }
            };
        }
        public IEnumerable<Game> GetAllGames()
        {
            return Games;
        }

        public Game GetGame(int id)
        {
            return Games.FirstOrDefault(game => game.GameId == id);
        }

        public Task AddGame(Game game)
        {
            throw new System.NotImplementedException();
        }
    }
}