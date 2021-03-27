using GameLib.Model.Entity;
using GameLib.Repository;

namespace GameLib.Service
{
    public class GameService : GenericService<IGameRepository, Game>, IGameService
    {
        public GameService(IGameRepository repository) : base(repository)
        {

        }
    }
}