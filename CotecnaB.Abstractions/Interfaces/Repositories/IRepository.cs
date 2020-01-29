using CotecnaB.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CotecnaB.Abstractions.Interfaces.Repositories
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        TEntity Find(Guid Id);
        TEntity GetSingle(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> GetFiltered(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> GetAll();

        void Create(TEntity entity);
        void Create(IEnumerable<TEntity> entities);

        void Delete(Guid Id);
        void Delete(TEntity entity);
        void Delete(IEnumerable<TEntity> entities);

        Task<TEntity> FindAsync(Guid Id);
        Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> GetFilteredAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> GetAllAsync();
    }
}
