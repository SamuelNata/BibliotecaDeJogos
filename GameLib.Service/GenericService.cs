using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameLib.Model;
using GameLib.Repository;

namespace GameLib.Service
{
    public abstract class GenericService<TRespository, TEntity> : IGenericService<TEntity>
        where TEntity : class, IBaseEntity
        where TRespository : IGenericRepository<TEntity>
    {
        protected internal TRespository Repository { get; set; }

        protected GenericService(TRespository respository)
        {
            Repository = respository;
        }

        public virtual Task<int> Save(TEntity obj)
        {
            if(obj.Id == null)
            {
                obj = (TEntity)obj.WithId();
            }
            
            return Repository.Save(obj);
        }

        public int SaveAll(IEnumerable<TEntity> obj)
        {
            return Repository.SaveAll(obj);
        }

        public virtual Task<int> Update(TEntity obj)
        {
            return Repository.Update(obj);
        }

        public virtual ValueTask<TEntity> GetById(Guid id)
        {
            return Repository.GetById(id);
        }

        public Task<List<TEntity>> GetAll()
        {
            return Repository.GetAll();
        }

        public virtual Task<int> Remove(TEntity obj)
        {
            return Repository.Remove(obj);
        }

        public async Task<TEntity> FindByIdOrFail(Guid id)
        {
            var model = await GetById(id);

            if (model == null)
            {
                throw new Exception("Registro não encontrado.");
            }

            return model;
        }
    }
}