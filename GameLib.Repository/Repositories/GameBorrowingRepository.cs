using GameLib.Model.Entity;
using GameLib.Repository.DbContext;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using GameLib.Model.DTOs;
using Npgsql;

namespace GameLib.Repository
{
    public class GameBorrowingRepository : GenericRepository<GameBorrowing, GameLibDbContext>, IGameBorrowingRepository
    {
        public GameBorrowingRepository(GameLibDbContext context) : base(context) {

        }

        public Task<List<GameBorrowingDTO>> GetHistoryBorrowedByOwner(Guid ownerId)
        {
            string query = @"
                SELECT
                    gb.id AS borrowing_id,
                    g.name AS game,
                    u.name AS borrower,
                    gb.start_date AS borrow_date,
                    gb.predicted_end_date AS expected_devolution_date,
                    gb.real_end_date AS real_devolution_date
                FROM game_borrowings gb
                    INNER JOIN user_games ug ON ug.id = gb.game_ownership_id
                    INNER JOIN games g ON g.id = ug.game_id
                    INNER JOIN ""AspNetUsers"" u ON u.id = gb.game_borrower_id
                WHERE ug.user_id = @owner_id
                ORDER BY gb.real_end_date DESC 
            ";

            var parans = new [] {
                new NpgsqlParameter("owner_id", ownerId)
            };
            return Context.Set<GameBorrowingDTO>().FromSqlRaw(query, parans).ToListAsync();
        }
    }
}