using GamesApi.Domain.Models;
using GamesApi.Domain.Repositores;
using GamesApi.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesApi.Services
{
    public class GameService : IGameService
    {
        public GameService(IGameRepository gameRepository)
        {
            GameRepository = gameRepository;
        }
        private IGameRepository GameRepository { get; }

        public async Task AddGame(Game game)
        {
            await GameRepository.AddGame(game);
        }

        public async Task AddGameImageAsync(GameImage gameImage)
        {
            await GameRepository.AddGameImageAsync(gameImage);
        }

        public async Task DeleteGame(Game game)
        {
            await GameRepository.DeleteGame(game);
        }

        public Task<IEnumerable<Game>> GetAllGames()
        {
            return GameRepository.GetAllGames();
        }

        public async Task<Game> GetGame(int id)
        {
            return await GameRepository.GetGame(id);
        }

        public Task<GameImage> GetGameImageAsync(int gameId)
        {
            return GameRepository.GetGameImageAsync(gameId);
        }

        public async Task<Game> GetLastGame()
        {
            return await GameRepository.GetLastGame();
        }

        public Task SaveChangesAsync()
        {
            return GameRepository.SaveChangesAsync();
        }
    }
}