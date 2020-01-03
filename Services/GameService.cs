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

        public IEnumerable<Game> GetAllGames()
        {
            return GameRepository.GetAllGames();
        }

        public Game GetGame(int id)
        {
            return GameRepository.GetGame(id);
        }
    }
}