using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ArandaEntity
{
    public interface IDisconGenericRepository<TEntity> where TEntity : class
    {
        int Add(IEnumerable<TEntity> newEntities);
        int Add(TEntity newEntity);
        Task<int> AddAsync(IEnumerable<TEntity> newEntities);
        Task<int> AddAsync(TEntity newEntity);
        IEnumerable<TEntity> All();
        Task<IEnumerable<TEntity>> AllAsync();
        TEntity Find(params object[] pks);
        Task<TEntity> FindAsync(params object[] pks);
        IEnumerable<TEntity> GetData(Expression<Func<TEntity, bool>> filter);
        Task<IEnumerable<TEntity>> GetDataAsync(Expression<Func<TEntity, bool>> filter);
        int Remove(IEnumerable<TEntity> removeEntities);
        int Remove(params object[] pks);
        int Remove(TEntity removeEntity);
        Task<int> RemoveAsync(IEnumerable<TEntity> removeEntities);
        Task<int> RemoveAsync(params object[] pks);
        Task<int> RemoveAsync(TEntity removeEntity);
        int Update(TEntity updateEntity);
        Task<int> UpdateAsync(TEntity updateEntity);
    }
}