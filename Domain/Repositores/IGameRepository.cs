using GamesApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesApi.Domain.Repositores
{
    public interface IGameRepository
    {
        public IEnumerable<Game> GetAllGames();
        public Game GetGame(int id);
        public Task AddGame(Game game);
    }
}