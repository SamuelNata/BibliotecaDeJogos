using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameLib.Model.Entity;
using GameLib.Repository.DbContext;
using Microsoft.EntityFrameworkCore;

namespace GameLib.Repository
{
    public class GameRepository : GenericRepository<Game, GameLibDbContext>, IGameRepository
    {
        public GameRepository(GameLibDbContext context) : base(context) {

        }

        public Task<List<Game>> SearchByNameAndYear(string name, int? year = null, bool exactName = false)
        {
            var query = Context.Game.AsQueryable();

            if(string.IsNullOrEmpty(name))
            {
                if(exactName)
                {
                    query = query.Where(g => g.Name.ToUpper() == name.ToUpper());
                }
                else
                {
                    query = query.Where(g => g.Name.ToUpper().Contains(name.ToUpper()));
                }
            }

            if(year.HasValue)
            {
                query = query.Where(g => g.Year == year.Value);
            }
            
            return query.ToListAsync();
        }
    }
}