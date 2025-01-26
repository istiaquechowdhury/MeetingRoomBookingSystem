using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MeetingRoomBooking.Domain.RepoInterface
{
    public interface IRepositoryBase<TEntity, TKey>
        where TEntity : class
        where TKey : IComparable
    {
       
        void Add(TEntity entity);
        Task AddAsync(TEntity entity);
        TEntity GetById(TKey id);
        Task<TEntity> GetByIdAsync(TKey id);
        IList<TEntity> GetAll();
        Task<IList<TEntity>> GetAllAsync();
        void Edit(TEntity entityToUpdate);
        void Remove(TKey id);
        void Remove(TEntity entityToDelete);

    }
}
