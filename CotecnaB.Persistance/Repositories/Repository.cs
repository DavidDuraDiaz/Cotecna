using CotecnaB.Abstractions.Interfaces.Repositories;
using CotecnaB.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CotecnaB.Persistance.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly DbContext _context;
        protected DbSet<TEntity> _entities;

        public Repository(DbContext context)
        {
            _context = context;
            _entities = _context.Set<TEntity>();
        }

        public TEntity Find(Guid Id)
        {
            return _entities.Find(Id);
        }
        public TEntity GetSingle(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.Where(predicate).FirstOrDefault();
        }
        public IEnumerable<TEntity> GetFiltered(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.Where(predicate).ToList();
        }
        public IEnumerable<TEntity> GetAll()
        {
            return _entities.ToList();
        }

        public void Create(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }
        public void Create(IEnumerable<TEntity> entities)
        {
            _entities.AddRange(entities);
        }

        public void Delete(Guid Id)
        {
            TEntity entity = Find(Id);
            Delete(entity);
        }
        public void Delete(TEntity entity)
        {
            _entities.Remove(entity);
        }
        public void Delete(IEnumerable<TEntity> entities)
        {
            _entities.RemoveRange(entities);
        }

        public async Task<TEntity> FindAsync(Guid Id)
        {
            return await _entities.FindAsync(Id);
        }
        public async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _entities.Where(predicate).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<TEntity>> GetFilteredAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _entities.Where(predicate).ToListAsync();
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _entities.ToListAsync();
        }
    }
}
