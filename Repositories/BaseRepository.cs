using GamesApi.Persistance;

namespace GamesApi.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly GameRepositoryDBContext _context;
        public BaseRepository(GameRepositoryDBContext context)
        {
            _context = context;
        }
    }
}