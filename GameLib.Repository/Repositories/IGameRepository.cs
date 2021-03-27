using System.Collections.Generic;
using System.Threading.Tasks;
using GameLib.Model.Entity;
using GameLib.Repository;

namespace GameLib.Repository
{
    public interface IGameRepository : IGenericRepository<Game>
    {
        Task<List<Game>> SearchByNameAndYear(string name, int? year = null, bool exactName = false);
    }
}