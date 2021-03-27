using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameLib.Model.Entity;
using GameLib.Model.DTOs;

namespace GameLib.Repository
{
    public interface IGameBorrowingRepository : IGenericRepository<GameBorrowing>
    {
        Task<List<GameBorrowingDTO>> GetHistoryBorrowedByOwner(Guid ownerId);
    }
}