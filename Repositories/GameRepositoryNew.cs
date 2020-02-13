using GamesApi.Persistance;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamesApi.Domain.Repositores;
using GamesApi.Domain.Models;
using System;
using System.IO;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IEnumerable<Game>> GetAllGames()
        {
            return await _context.Games.Include(g => g.GameImage).ToListAsync();
        }
        public async Task<Game> GetGame(int id)
        {
            return await _context.Games.Include(g => g.GameImage).FirstOrDefaultAsync(g => g.GameId == id);
        }
        public async Task DeleteGame(Game gameToDelete)
        {
            _context.Games.Remove(gameToDelete);
            await _context.SaveChangesAsync();
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task AddGameImageAsync(GameImage gameImage)
        {
            await _context.GameImages.AddAsync(gameImage);
            await _context.SaveChangesAsync();
        }
        public async Task<GameImage> GetGameImageAsync(int gameId)
        {
            return await _context.GameImages.FindAsync(gameId);
        }
        public async Task<Game> GetLastGame()
        {
            return await _context.Games.OrderByDescending(g => g.GameId).FirstOrDefaultAsync();
        }
    }
}