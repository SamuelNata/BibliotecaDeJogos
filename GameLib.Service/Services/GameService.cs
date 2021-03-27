using System.Linq;
using System.Threading.Tasks;
using GameLib.Model.Entity;
using GameLib.Model.Exception;
using GameLib.Repository;

namespace GameLib.Service
{
    public class GameService : GenericService<IGameRepository, Game>, IGameService
    {
        public GameService(IGameRepository repository) : base(repository)
        {

        }

        public override async Task<int> Save(Game obj)
        {
            var games = await Repository.SearchByNameAndYear(obj.Name, obj.Year, true);
            if(games != null && games.Any())
            {
                throw new AlredyExistFException("Já existe um jogo com esse nome no catalogo.");
            }

            return await Repository.Save(obj);
        }
    }
}