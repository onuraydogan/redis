using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ykbredis.core.data
{
    public class IRepositoryBase<TEntity, TContext> : IRepository<TEntity>
       where TEntity : class, IEntity, new()
       where TContext : DbContext, new()
    {
        public TEntity Get(long id, params Expression<Func<TEntity, object>>[] includes)
        {
            using (var context = new TContext())
            {
                IQueryable<TEntity> set = context.Set<TEntity>();
                foreach (var includeProperty in includes)
                {
                    set = set.Include(includeProperty);
                }
                return set.First(p => p.Id == id);
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includes)
        {
            using (var context = new TContext())
            {
                IQueryable<TEntity> set = context.Set<TEntity>();
                foreach (var includeProperty in includes)
                {
                    set = set.Include(includeProperty);
                }
                return set.Where(p => p.IsDeleted == false).Where(filter).FirstOrDefault();
            }
        }

        public List<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includes)
        {
            using (var context = new TContext())
            {
                IQueryable<TEntity> set = context.Set<TEntity>();
                foreach (var includeProperty in includes)
                {
                    set = set.Include(includeProperty);
                }

                return filter == null
                    ? set.Where(p => p.IsDeleted == false).ToList()
                    : set.Where(p => p.IsDeleted == false).Where(filter).ToList();
            }
        }


        public int Count(Expression<Func<TEntity, bool>> filter = null)
        {
            using (var context = new TContext())
            {
                IQueryable<TEntity> set = context.Set<TEntity>();

                return filter == null
                    ? set.Where(p => p.IsDeleted == false).Count()
                    : set.Where(p => p.IsDeleted == false).Where(filter).Count();
            }
        }

        public int Sum(Expression<Func<TEntity, int>> column, Expression<Func<TEntity, bool>> filter = null)
        {
            using (var context = new TContext())
            {
                IQueryable<TEntity> set = context.Set<TEntity>();

                return filter == null
                    ? set.Where(p => p.IsDeleted == false).Sum(column)
                    : set.Where(p => p.IsDeleted == false).Where(filter).Sum(column);
            }
        }
        public long Sum(Expression<Func<TEntity, long>> column, Expression<Func<TEntity, bool>> filter = null)
        {
            using (var context = new TContext())
            {
                IQueryable<TEntity> set = context.Set<TEntity>();

                return filter == null
                    ? set.Where(p => p.IsDeleted == false).Sum(column)
                    : set.Where(p => p.IsDeleted == false).Where(filter).Sum(column);
            }
        }
        public decimal Sum(Expression<Func<TEntity, decimal>> column, Expression<Func<TEntity, bool>> filter = null)
        {
            using (var context = new TContext())
            {
                IQueryable<TEntity> set = context.Set<TEntity>();

                return filter == null
                    ? set.Where(p => p.IsDeleted == false).Sum(column)
                    : set.Where(p => p.IsDeleted == false).Where(filter).Sum(column);
            }
        }

        public double Sum(Expression<Func<TEntity, double>> column, Expression<Func<TEntity, bool>> filter = null)
        {
            using (var context = new TContext())
            {
                IQueryable<TEntity> set = context.Set<TEntity>();

                return filter == null
                    ? set.Where(p => p.IsDeleted == false).Sum(column)
                    : set.Where(p => p.IsDeleted == false).Where(filter).Sum(column);
            }
        }

        public void Add(TEntity entity)
        {
            using (var context = new TContext())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void Update(TEntity entity)
        {
            using (var context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void Delete(TEntity entity)
        {
            using (var context = new TContext())
            {
                entity.IsDeleted = true;
                entity.IsActive = false;

                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }

}
