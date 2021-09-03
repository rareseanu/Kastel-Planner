using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Base;

namespace Application.RepositoryInterfaces
{
    public interface IRepository<TEntity> where TEntity : BasicEntity
    {
        Task<List<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes);

        Task<List<TEntity>> GetAllByPredicateAsync(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includes);

        Task<TEntity> GetByIdAsync(Guid entityId);

        Task<TEntity> GetFirstByPredicateAsync(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includes);

        Task<TEntity> AddAsync(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(TEntity entity);
    }
}
