using GameLib.Model.Entity;
using GameLib.Repository.DbContext;

namespace GameLib.Repository
{
    public class GameRepository : GenericRepository<Game, GameLibDbContext>, IGameRepository
    {
        public GameRepository(GameLibDbContext context) : base(context) {

        }
    }
}