using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameLib.Model.DTOs;
using GameLib.Model.Entity;
using GameLib.Model.Exception;
using GameLib.Repository;

namespace GameLib.Service
{
    public class UserGameService : GenericService<IUserGameRepository, UserGame>, IUserGameService
    {
        public UserGameService(IUserGameRepository repository) : base(repository)
        {
            
        }

        public Task<List<UserGame>> SearchByGameAndOwner(
            Guid ownerId,
            Guid? gameId = null,
            bool onlyBorrowable = false
        ){
            return Repository.SearchByGameAndOwner(ownerId, gameId, onlyBorrowable);
        }

        public Task<List<GameInfoDTO>> SearchGamesBy(Guid? userId = null, Guid? gameId = null, bool? isBorrowed = null)
        {
            return Repository.SearchGamesBy(userId, gameId, isBorrowed);
        }

        public async Task<int> RemoveGameFromUser(Guid userGameId, Guid ownerId)
        {
            var gameOwnerships = await Repository.GetById(userGameId, g => g.GameBorrowings);
            if(gameOwnerships == null || gameOwnerships.UserId != ownerId)
            {
                throw new BusinessRuleFException("Você não possui este jogo para excluir");
            }

            if(gameOwnerships.GameBorrowings.Any(gb => gb.RealEndDate == null))
            {
                throw new BusinessRuleFException("Este jogo está emprestado no momento, portanto não pode ser excluido");
            }

            return await Remove(gameOwnerships);
        }
    }
}