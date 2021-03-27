using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using GameLib.Model.Entity;
using GameLib.Repository.DbContext;

namespace GameLib.Repository
{
    public class UserGameRepository : GenericRepository<UserGame, GameLibDbContext>, IUserGameRepository
    {
        public UserGameRepository(GameLibDbContext context) : base(context) {

        }
        
        public override async ValueTask<UserGame> GetById(Guid id)
        {
            return await Context.UserGame
                .Where(ug => ug.Id == id)
                .Include(ug => ug.Game)
                .Include(ug => ug.User)
                .FirstOrDefaultAsync();
        }

        public async Task<List<UserGame>> SearchByGameAndOwner(
            Guid ownerId,
            Guid? gameId = null,
            bool onlyBorrowable = false
        ) {
            var query = Context.UserGame.Where(go => go.User.Id == ownerId);
            
            if(gameId.HasValue)
            {
                query = query.Where(go => go.Game.Id == gameId);
            }

            if(onlyBorrowable){
                query = query.Where(go => !go.GameBorrowings.Any(gb => gb.RealEndDate == null));
            }

            return await query
                .Include(go => go.Game)
                .Include(go => go.User)
                .ToListAsync();
        }
    
        public Task<List<Game>> SearshGamesByUser(Guid userId){
            return Context.UserGame.Where(go => go.User.Id == userId).Select(go => go.Game).ToListAsync();
        }
    }
}