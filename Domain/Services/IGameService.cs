using GamesApi.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GamesApi.Domain.Services
{
    public interface IGameService
    {
        public IEnumerable<Game> GetAllGames();
        public Game GetGame(int id);
        public Task AddGame(Game game);
    }
}