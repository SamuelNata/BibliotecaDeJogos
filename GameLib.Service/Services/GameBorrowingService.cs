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
    public class GameBorrowingService : GenericService<IGameBorrowingRepository, GameBorrowing>, IGameBorrowingService
    {
        private readonly IUserGameRepository _userGameRepository;

        public GameBorrowingService(
            IGameBorrowingRepository repository,
            IUserGameRepository userGameRepository) : base(repository)
        {
            _userGameRepository = userGameRepository;
        }

        public async Task<int> BorrowGame(Guid gameId, Guid ownerId, Guid borrowerId, DateTime predictedDevolution)
        {
            var gamesOwnership = await _userGameRepository.SearchByGameAndOwner(ownerId, gameId, onlyBorrowable: true);
            if(gamesOwnership.Any()){
                var gameToBorrow = gamesOwnership.First();
                var borrowing = new GameBorrowing{
                    GameBorrowerId = borrowerId,
                    GameOwnershipId = gameToBorrow.Id,
                    StartDate = DateTime.Now,
                    PredictedEndDate = predictedDevolution,
                    RealEndDate = null
                };
                return await Save(borrowing);
            }

            throw new NotFoundException("Parece que esse usuário não possui o jogo para emprestar.");
        }

        public async Task<int> BorrowGame(Guid gameOwnershipId, Guid borrowerId, DateTime predictedDevolution)
        {
            var gameOwnership = await _userGameRepository.GetById(gameOwnershipId, g => g.Game, g => g.User);
            return await BorrowGame(gameOwnership.Game.Id, gameOwnership.User.Id, borrowerId, predictedDevolution);
        }

        public Task<List<GameBorrowingDTO>> GetHistoryBorrowedByOwner(Guid ownerId){
            return Repository.GetHistoryBorrowedByOwner(ownerId);
        }
    
        public async Task<int> MarkAsReturned(Guid id)
        {
            var target = await GetById(id);
            if(target != null){
                target.RealEndDate = DateTime.Now;
                return await Update(target);
            }
            throw new NotFoundException($"Emprestimo de id '{id}' não foi encontrado");
        }
    }
}