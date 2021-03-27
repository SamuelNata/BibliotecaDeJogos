using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameLib.Model.Entity;

namespace GameLib.Repository
{
    public interface IUserRepository : IGenericRepository<User>
    { 
        Task<User> FindToLogin(string username, string passswordMD5);
        Task<User> GetByUsername(string username);
    }
}