using System.Threading.Tasks;
using GameLib.Model.Entity;

namespace GameLib.Service
{
    public interface IUserService : IGenericService<User>
    {
        Task<User> FindToLogin(string username, string passswordMD5);

        Task<User> CreateNewUser(User user, string plainTextPassword);
    }
}