using System;
using Xunit;
using System.Threading.Tasks;
using System.Linq;
using GameLib.Service;
using GameLib.Repository;
using GameLib.Model.Entity;
using GameLib.Model.Exception;

namespace GameLib.Test
{
    public class UserServiceTests : InMemoryDbTest
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserService _sut;

        public UserServiceTests(){
            _userRepository = new UserRepository(DbContext);
            _sut = new UserService(_userRepository);
        }


        [Fact]
        public async Task DatabaseIsAvailableAndCanBeConnectedTo()
        {
            Assert.True(await DbContext.Database.CanConnectAsync());
        }

        [Fact]
        public async Task CantCreateAccountWithUsernameAlredyTaken()
        {
            var user1 = new User{ Nickname = "nickname1", Username = "username", Password = "MD5_password1" };
            var user2 = new User{ Nickname = "nickname2", Username = "username", Password = "MD5_password2" };

            Assert.NotNull(await _sut.CreateNewUser(user1, "passwordEx"));
            await Assert.ThrowsAsync<AlredyExistFException>(() => _sut.CreateNewUser(user2, "passwordEx"));
        }
    }
}
