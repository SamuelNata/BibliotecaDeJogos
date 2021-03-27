using System;
using Xunit;
using System.Threading.Tasks;
using GameLib.Service;
using GameLib.Repository;
using GameLib.Model.Exception;
using GameLib.Model.Entity;

namespace GameLib.Test
{
    public class GameServiceTests : InMemoryDbTest
    {
        private readonly IGameService _sut;

        public GameServiceTests()
        {
            var _gameRepository = new GameRepository(DbContext);
            _sut = new GameService(_gameRepository);
        }


        [Fact]
        public async Task DatabaseIsAvailableAndCanConnected()
        {
            Assert.True(await DbContext.Database.CanConnectAsync());
        }

        [Fact]
        public async Task CantInsertNewGameIfExistsSameNameAndYear()
        {
            var game1 = new Game { Name = "game", Year = 1 };
            var game2 = new Game { Name = "game", Year = 1 };
            
            Assert.NotNull(await _sut.Save(game1));
            await Assert.ThrowsAsync<AlredyExistFException>(() => _sut.Save(game2));
        }
    }
}
