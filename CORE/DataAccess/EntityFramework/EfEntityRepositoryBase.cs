using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Entities.Concrete;
using Core.Entities.Concrete.RequestFeatures;
using Core.Entities.Abstract;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TRequestParameters> : IRepositoryBase<TEntity, TRequestParameters>
        where TEntity : class, IEntity, new()
        where TRequestParameters : RequestParameters, new()
    {
        protected DbContext _context;
        public EfEntityRepositoryBase(DbContext context)
        {
            _context = context;
        }

        public void Add(TEntity entity) => _context.Set<TEntity>().Add(entity);
        public void Delete(TEntity entity) => _context.Set<TEntity>().Remove(entity);
        public void Update(TEntity entity) => _context.Set<TEntity>().Update(entity);
        
        
        public TEntity Get(Expression<Func<TEntity, bool>> filter, bool trackChanges)
            => GetAllAsQueryable(trackChanges).SingleOrDefault(filter);
       
        
        public PagedList<TEntity> GetAll(TRequestParameters requestParameters, bool trackChanges)
        {
            var query = GetAllAsQueryable(trackChanges);

            return PagedList<TEntity>.ToPagedList(query, requestParameters.PageNumber, requestParameters.PageSize);
        }
        public PagedList<TEntity> GetAllByCondition(Expression<Func<TEntity, bool>> filter, TRequestParameters requestParameters, bool trackChanges)
        {
            var query = GetAllByConditionAsQueryable(filter, trackChanges);

            return PagedList<TEntity>.ToPagedList(query, requestParameters.PageNumber, requestParameters.PageSize);
        }
        
        
        public IEnumerable<TEntity> GetAllAsEnumerable(bool trackChanges)
            => GetAllAsQueryable(trackChanges);                
        public IEnumerable<TEntity> GetAllByConditionAsEnumerable(Expression<Func<TEntity, bool>> filter, bool trackChanges)
            => GetAllByConditionAsQueryable(filter,trackChanges);
        

        protected IQueryable<TEntity> GetAllAsQueryable(bool trackChanges)
            =>  !trackChanges
                ? _context.Set<TEntity>().AsNoTracking()
                : _context.Set<TEntity>();
        protected IQueryable<TEntity> GetAllByConditionAsQueryable(Expression<Func<TEntity, bool>> filter, bool trackChanges)
            => GetAllAsQueryable(trackChanges).Where(filter);       

    }
}
