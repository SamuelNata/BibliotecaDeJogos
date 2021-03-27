using System;
using GameLib.Repository.DbContext;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace GameLib.Test
{
    public abstract class InMemoryDbTest : IDisposable
    {
        private const string InMemoryConnectionString = "DataSource=:memory:";
        private readonly SqliteConnection _connection;

        protected readonly GameLibDbContext DbContext;

        protected InMemoryDbTest()
        {
            _connection = new SqliteConnection(InMemoryConnectionString);
            _connection.Open();
            var options = new DbContextOptionsBuilder<GameLibDbContext>()
                    .UseSqlite(_connection)
                    .Options;
            DbContext = new GameLibDbContext(options);
            DbContext.Database.EnsureCreated();
        }

        public void Dispose()
        {
            _connection.Close();
        }

        public void AddRandomUsers(int quantity){
            while(quantity-- > 0){
                DbContext.User.Add(new GameLib.Model.Entity.User
                {
                    Username = $"user-{quantity}@email.com",
                    Nickname = $"user-{quantity}",
                    Password = "abcdefgijokl"
                });
            }
            DbContext.SaveChanges();
        }

        public void AddRandomGames(int quantity){
            while(quantity-- > 0){
                DbContext.Game.Add(new GameLib.Model.Entity.Game
                {
                    Name = $"game-{quantity}",
                    Year = (short)quantity
                });
            }
            DbContext.SaveChanges();
        }
    }
}