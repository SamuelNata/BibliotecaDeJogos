using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        /// <summary>
        /// Get the list of games one user
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns></returns>
        Task<List<Game>> SearshGamesByUser(Guid userId);
    }

    
}