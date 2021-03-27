

using System.Threading.Tasks;
using GameLib.Model.Entity;
using GameLib.Model.Exception;
using GameLib.Repository;

namespace GameLib.Service
{
    public class UserService : GenericService<IUserRepository, User>, IUserService
    {
        public UserService(IUserRepository repository) : base(repository)
        {
            
        }

        public Task<User> FindToLogin(string username, string passswordMD5)
        {
            return Repository.FindToLogin(username, passswordMD5);
        }

        public override Task<int> Save(User user)
        {
            throw new System.Exception("This method shold not be used, use 'CreateNewUser' instead.");
        }

        public async Task<User> CreateNewUser(User user, string plainTextPassword)
        {
            var dbUser = await Repository.GetByUsername(user.Username);
            if(dbUser != null)
            {
                throw new AlredyExistFException("Esse nome de usuário já está em uso");
            }

            ValidatePassword(plainTextPassword);

            await Repository.Save(user);
            return user;
        }

        public bool ValidatePassword(string password, bool throwOnFail = true)
        {
            if(password.Length < 8)
            {
                if(throwOnFail)
                {
                    throw new InvalidPasswordFException("Sua senha deve conter no minimo 8 dígitos");
                }
                else
                {
                    return false;
                }
            }

            return true;
        }
    }
}