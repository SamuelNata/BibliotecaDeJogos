using GameLib.Repository.DbContext;
using GameLib.Model.Entity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GameLib.Repository
{
    public class UserRepository : GenericRepository<User, GameLibDbContext>, IUserRepository
    {
        public UserRepository(GameLibDbContext context) : base(context) {

        }

        public Task<User> FindToLogin(string username, string passswordMD5)
        {
            return Context.User.SingleOrDefaultAsync(u => u.Username == username && u.Password == passswordMD5);
        }

        public Task<User> GetByUsername(string username)
        {
            return Context.User.SingleOrDefaultAsync(u => u.Username == username);
        }
    }
}