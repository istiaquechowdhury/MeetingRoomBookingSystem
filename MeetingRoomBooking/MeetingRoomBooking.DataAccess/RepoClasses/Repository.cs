using MeetingRoomBooking.DataAccess.RepoClasses;
using MeetingRoomBooking.Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Globalization;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Inventory.Infrastructure.RepositoryPatternClasses
{
    public abstract class Repository<TEntity, TKey>
        : IRepository<TEntity, TKey> where TKey : IComparable
        where TEntity : class, IEntity<TKey>
    {
        private DbContext _dbContext;
        private DbSet<TEntity> _dbSet;

        public Repository(DbContext context)
        {
            _dbContext = context;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public virtual async Task AddAsync(TEntity entity) //this is the code
        {
            await _dbSet.AddAsync(entity);
        }

       
        public virtual async Task<TEntity> GetByIdAsync(TKey id) //this is the code
        {
            return await _dbSet.FindAsync(id);
        }

      

        public virtual async Task<IList<TEntity>> GetAllAsync() //this is the code
        {
            IQueryable<TEntity> query = _dbSet;
            return await query.ToListAsync();
        }

       
        public virtual void Add(TEntity entity) ///This is the code
        {
            _dbSet.Add(entity);
        }

        public virtual void Remove(TKey id) //this is the code
        {
            var entityToDelete = _dbSet.Find(id);
            Remove(entityToDelete);
        }

        public virtual void Remove(TEntity entityToDelete)//this is the code
        {
            if (_dbContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }
            _dbSet.Remove(entityToDelete);
        }

        public virtual void Remove(Expression<Func<TEntity, bool>> filter)  //this is the code
        {
            _dbSet.RemoveRange(_dbSet.Where(filter));
        }

        public virtual void Edit(TEntity entityToUpdate) //this is the code
        {
            _dbSet.Attach(entityToUpdate);
            _dbContext.Entry(entityToUpdate).State = EntityState.Modified;
        }

       
        public virtual IList<TEntity> GetAll() //this is the code
        {
            IQueryable<TEntity> query = _dbSet;
            return query.ToList();
        }

        public virtual TEntity GetById(TKey id) //this is the code
        {
            return _dbSet.Find(id);
        }

       
        public virtual (IList<TEntity> data, int total, int totalDisplay) GetDynamic( //this is the code
            Expression<Func<TEntity, bool>> filter = null,
            string orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            int pageIndex = 1, int pageSize = 10, bool isTrackingOff = false)
        {
            IQueryable<TEntity> query = _dbSet;
            var total = query.Count();
            var totalDisplay = query.Count();

            if (filter != null)
            {
                query = query.Where(filter);
                totalDisplay = query.Count();
            }

            if (include != null)
                query = include(query);

            if (orderBy != null)
            {
                var result = query.OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize);
                if (isTrackingOff)
                    return (result.AsNoTracking().ToList(), total, totalDisplay);
                else
                    return (result.ToList(), total, totalDisplay);
            }
            else
            {
                var result = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
                if (isTrackingOff)
                    return (result.AsNoTracking().ToList(), total, totalDisplay);
                else
                    return (result.ToList(), total, totalDisplay);
            }
        }

      

    }
}