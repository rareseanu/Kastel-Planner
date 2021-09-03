using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Application.RepositoryInterfaces;
using Domain.Base;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public abstract class Repository<TEntity>
       : IRepository<TEntity>
       where TEntity : BasicEntity
    {
        protected Repository([NotNull] KastelPlannerDbContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        private DbContext Context { get; }
        private DbSet<TEntity> Entities => Context.Set<TEntity>();
        private IQueryable<TEntity> EntityQuery => DefaultIncludes(Entities);


        public virtual Task<List<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes)
        {
            return EntityQuery
                .Includes(includes)
                .ToListAsync();
        }

        public virtual Task<List<TEntity>> GetAllByPredicateAsync(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includes)
        {
            return EntityQuery
                .Includes(includes)
                .Where(predicate)
                .ToListAsync();
        }

        public virtual Task<TEntity> GetByIdAsync(Guid entityId)
        {
            return EntityQuery.Where(q => q.Id.Equals(entityId))
                .FirstOrDefaultAsync();
        }

        public Task<TEntity> GetFirstByPredicateAsync(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includes)
        {
            return EntityQuery.Includes(includes)
                .Where(predicate)
                .FirstOrDefaultAsync();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var result = await Entities.AddAsync(entity);
            await Context.SaveChangesAsync();
            return result.Entity;
        }


        public async Task Update(TEntity entity)
        {
            Context.Update(entity);
            await Context.SaveChangesAsync();
        }

        public async Task Delete(TEntity entity)
        {
            Context.Remove(entity);
            await Context.SaveChangesAsync();
        }

        protected virtual IQueryable<TEntity> DefaultIncludes(IQueryable<TEntity> queryable)
        {
            return queryable;
        }
    }
}
