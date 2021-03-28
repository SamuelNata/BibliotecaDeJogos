using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GameLib.Model.DTOs;
using GameLib.Model.Entity;

namespace GameLib.Repository
{
    public interface IUserGameRepository : IGenericRepository<UserGame>
    { 
        /// <summary>
        /// Search a list of games by game owner and optionaly by gameId to
        /// </summary>
        /// <param name="ownerId">Id of user that own the game</param>
        /// <param name="gameId">Id of the game</param>
        /// <param name="onlyBorrowable">Flag to search only games available to borrow</param>
        /// <returns></returns>
        Task<List<UserGame>> SearchByGameAndOwner(Guid ownerId, Guid? gameId = null, bool onlyBorrowable = false);

        Task<List<GameInfoDTO>> SearchGamesBy(Guid? userId = null, Guid? gameId = null, bool? isBorrowed = null);

        ValueTask<UserGame> GetById(Guid id, params Expression<Func<UserGame,object>> [] includes);
    }
}