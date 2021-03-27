using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameLib.Service
{
    public interface IGenericService<TEntity> where TEntity : class
    {
        Task<int> Save(TEntity obj);
        int SaveAll(IEnumerable<TEntity> obj);
        Task<int> Update(TEntity obj);
        ValueTask<TEntity> GetById(Guid id);
        Task<List<TEntity>> GetAll();
        Task<int> Remove(TEntity obj);
        Task<TEntity> FindByIdOrFail(Guid id);
    }
}