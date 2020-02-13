using GamesApi.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GamesApi.Domain.Services
{
    public interface IGameService
    {
        Task<IEnumerable<Game>> GetAllGames();
        Task<Game> GetGame(int id);
        Task<Game> GetLastGame();
        Task AddGame(Game game);
        Task DeleteGame(Game game);
        Task SaveChangesAsync();
        Task<GameImage> GetGameImageAsync(int gameId);
        Task AddGameImageAsync(GameImage gameImage);
    }
}