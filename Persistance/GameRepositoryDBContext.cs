using GamesApi.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace GamesApi.Persistance
{
    public class GameRepositoryDBContext : DbContext
    {
        private readonly IConfiguration _config;
        public DbSet<Game> Games { get; set; }
        public DbSet<GameImage> GameImages { get; set; }
        public GameRepositoryDBContext(DbContextOptions<GameRepositoryDBContext> options, IConfiguration config) :base(options)
        {
            _config = config;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config.GetConnectionString("GamesApiDB"));
        }
    }
}