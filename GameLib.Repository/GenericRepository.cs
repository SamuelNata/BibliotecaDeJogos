using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using GameLib.Repository.DbContext;
using GameLib.Model;

namespace GameLib.Repository
{
    public abstract class GenericRepository<TEntity, TContext> : IGenericRepository<TEntity>
        where TEntity : class, IBaseEntity
        where TContext : BaseDbContext
    {
        protected readonly TContext Context;

        protected GenericRepository(TContext context)
        {
            Context = context;
        }

        public Task<List<TEntity>> GetAll()
        {
            return Context.Set<TEntity>().ToListAsync();
        }

        public virtual ValueTask<TEntity> GetById(Guid id)
        {
            return Context.FindAsync<TEntity>(id);
        }

        public Task<int> Remove(TEntity obj)
        {
            Context.Remove(obj);
            return Context.SaveChangesAsync();
        }

        public int RemoveAll(List<TEntity> obj)
        {
            using (var transaction = Context.Database.BeginTransaction())
            {
                foreach (TEntity entity in obj)
                {
                    Context.Remove(entity);
                }
                int countChanges = Context.SaveChanges();
                transaction.Commit();
                return countChanges;
            }
        }

        public Task<int> Save(TEntity obj)
        {
            Context.Add(obj);
            return Context.SaveChangesAsync();
        }

        public int SaveAll(IEnumerable<TEntity> obj)
        {
            using (var transaction = Context.Database.BeginTransaction())
            {
                foreach (TEntity entity in obj)
                {
                    Context.Add(entity);
                }
                int countChanges = Context.SaveChanges();
                transaction.Commit();
                return countChanges;
            }
        }

        public async Task<int> SaveAllAsync(IEnumerable<TEntity> obj)
        {
            await Context.AddRangeAsync(obj);
            return await Context.SaveChangesAsync();
        }

        public Task<int> Update(TEntity obj)
        {
            Context.Update(obj);
            return Context.SaveChangesAsync();
        }

    }
}
