using GamesApi.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamesApi.Domain.Repositores;
using GamesApi.Domain.Models;

namespace GamesApi.Repositories
{
    public class GameRepositoryNew : BaseRepository, IGameRepository
    {
        public GameRepositoryNew(GameRepositoryDBContext context) :base(context)
        {

        }

        public async Task AddGame(Game game)
        {
            await _context.Games.AddAsync(game);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Game> GetAllGames()
        {
            return _context.Games.ToList();
        }

        public Game GetGame(int id)
        {
            throw new NotImplementedException();
        }
    }
}