using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameLib.Model.DTOs;
using GameLib.Model.Entity;

namespace GameLib.Service
{
    public interface IGameBorrowingService : IGenericService<GameBorrowing>
    {
        Task<int> BorrowGame(Guid gameId, Guid ownerId, Guid borrowerId, DateTime predictedDevolution);

        Task<int> BorrowGame(Guid gameOwnershipId, Guid borrowerId, DateTime predictedDevolution);
        
        Task<List<GameBorrowingDTO>> GetHistoryBorrowedByOwner(Guid ownerId);

        Task<int> MarkAsReturned(Guid id);
    }
}