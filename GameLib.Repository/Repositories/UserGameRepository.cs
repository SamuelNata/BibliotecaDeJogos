using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using GameLib.Model.Entity;
using GameLib.Repository.DbContext;
using GameLib.Model.DTOs;
using System.Linq.Expressions;

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
    
        public async Task<List<GameInfoDTO>> SearchGamesBy(Guid? userId = null, Guid? gameId = null, bool? isBorrowed = null)
        {
            var query = Context.UserGame.AsQueryable();
            
            if(userId.HasValue)
            {
                query = query.Where(go => go.UserId == userId.Value);
            }

            if(gameId.HasValue)
            {
                query = query.Where(go => go.GameId == gameId.Value);
            }

            if(isBorrowed.HasValue)
            {
                if(isBorrowed.Value)
                {
                    query = query.Where(go => go.GameBorrowings.Any(gb => gb.RealEndDate == null));
                }
                else
                {
                    query = query.Where(go => !go.GameBorrowings.Any(gb => gb.RealEndDate == null));
                }
            }

            var entityResult = await query
                .Include(x => x.Game)
                .Include(x => x.User)
                .Include(x => x.GameBorrowings)
                .ThenInclude(g => g.GameBorrower)
                .ToListAsync();
            
            var result = new List<GameInfoDTO>();

            entityResult.ForEach(x => {
                var gameBorrowing = x.GameBorrowings.FirstOrDefault(gb => gb.RealEndDate == null);
                result.Add(new GameInfoDTO
                {
                    OwnedGameRelationId = x.Id,
                    GameId = x.GameId,
                    GameName = x.Game.Name,
                    GameOwnerId = x.UserId,
                    GameOwnerName = x.User.Nickname,
                    CurrentBorrowingId = gameBorrowing?.Id,
                    BorrowDate = gameBorrowing?.StartDate,
                    ExpectedDevolutionDate = gameBorrowing?.PredictedEndDate,
                    RealDevolutionDate = gameBorrowing?.RealEndDate,
                    BorrowerId = gameBorrowing?.GameBorrowerId,
                    BorrowerName = gameBorrowing?.GameBorrower.Nickname
                });
            });

            return result;
        }
    
        public async ValueTask<UserGame> GetById(Guid id, params Expression<Func<UserGame,object>> [] includes)
        {
            var query = Context.UserGame.Where(x => x.Id == id);
            if(includes != null)
            {
                foreach(var includeFunc in includes){
                    query = query.Include(includeFunc);
                }
            }
            return await query.SingleAsync();
        }
    }
}