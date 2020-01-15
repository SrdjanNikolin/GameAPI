using GamesApi.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GamesApi.Domain.Services
{
    public interface IGameService
    {
        Task<IEnumerable<Game>> GetAllGames();
        Task<Game> GetGame(int id);
        Task AddGame(Game game);
        Task DeleteGame(Game game);
        Task SaveChangesAsync();
        Task AddGameImageAsync(GameImage gameImage);
        Task<GameImage> GetGameImageAsync(int gameId);
        Task UpdateGameImageAsync(GameImage gameImage, string newImage);
        string ConvertImagePathToBase64(string imagePath);
    }
}