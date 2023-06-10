using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Entities.Abstract;
using Microsoft.EntityFrameworkCore.Design;
using Entities.Concrete;
using Core.Entities.Concrete.RequestFeatures;

namespace Core.DataAccess.EntityFramework
{   
    public class EfEntityRepositoryBase<TEntity, TContext, TRequestParameters> : IRepositoryBase<TEntity, TRequestParameters>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
        where TRequestParameters : RequestParameters, new()
    {
        private readonly IDesignTimeDbContextFactory<TContext> _contextFactory;
        public EfEntityRepositoryBase(IDesignTimeDbContextFactory<TContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }
        public void Add(TEntity entity)
        {
            using (var context = _contextFactory.CreateDbContext(new String[0]))
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added; 
                context.SaveChanges();

            }
        }
        public void Delete(TEntity entity)
        {
            using (var context = _contextFactory.CreateDbContext(new String[0]))
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }
        public void Update(TEntity entity)
        {
            using (var context = _contextFactory.CreateDbContext(new String[0]))
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (var context = _contextFactory.CreateDbContext(new String[0]))
                return context.Set<TEntity>().SingleOrDefault(filter);

        }
        public PagedList<TEntity> GetAll(TRequestParameters requestParameters,Expression < Func<TEntity, bool>> filter = null)
        {
            using (var context = _contextFactory.CreateDbContext(new String[0]))
            {               
                List<TEntity> result;

                if (filter is null) 
                { 
                    result = context.Set<TEntity>().ToList();
                }
                else 
                { 
                    result = context.Set<TEntity>().Where(filter).ToList(); 
                }
                return PagedList<TEntity>.ToPagedList(result, requestParameters.PageNumber, requestParameters.PageSize);
                
            }
        }
        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (var context = _contextFactory.CreateDbContext(new String[0]))
            {                
                if (filter is null)                
                    return context.Set<TEntity>().ToList();
                
                else                
                    return context.Set<TEntity>().Where(filter).ToList();
                
            }
        }

    }
}
