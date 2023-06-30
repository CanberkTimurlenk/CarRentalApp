using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Entities.Concrete;
using Core.Entities.Concrete.RequestFeatures;
using Core.Entities.Abstract;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity> : IRepositoryBase<TEntity>
        where TEntity : class, IEntity, new()

    {
        protected DbContext _context;
        public EfEntityRepositoryBase(DbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TEntity entity) => await _context.Set<TEntity>().AddAsync(entity);
        public void Delete(TEntity entity) => _context.Set<TEntity>().Remove(entity);
        public void Update(TEntity entity) => _context.Set<TEntity>().Update(entity);

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter, bool trackChanges)
            => await GetAllAsQueryable(trackChanges).SingleOrDefaultAsync(filter);

        public async Task<PagedList<TEntity>> GetAllAsync(RequestParameters requestParameters, bool trackChanges)
        {
            var query = await GetAllAsQueryable(trackChanges).ToListAsync();

            return PagedList<TEntity>.ToPagedList(query, requestParameters.PageNumber, requestParameters.PageSize);
        }

        public async Task<PagedList<TEntity>> GetAllByConditionAsync(Expression<Func<TEntity, bool>> filter, RequestParameters requestParameters, bool trackChanges)

        {
            var query = await GetAllByConditionAsQueryable(filter, trackChanges).ToListAsync();

            return PagedList<TEntity>.ToPagedList(query, requestParameters.PageNumber, requestParameters.PageSize);
        }


        public async Task<IEnumerable<TEntity>> GetAllAsEnumerableAsync(bool trackChanges)
            => await GetAllAsQueryable(trackChanges).ToListAsync();
        public async Task<IEnumerable<TEntity>> GetAllByConditionAsEnumerableAsync(Expression<Func<TEntity, bool>> filter, bool trackChanges)
            => await GetAllByConditionAsQueryable(filter, trackChanges).ToListAsync();


        protected IQueryable<TEntity> GetAllAsQueryable(bool trackChanges)
            => !trackChanges
                ? _context.Set<TEntity>().AsNoTracking()
                : _context.Set<TEntity>();
        protected IQueryable<TEntity> GetAllByConditionAsQueryable(Expression<Func<TEntity, bool>> filter, bool trackChanges)
            => GetAllAsQueryable(trackChanges).Where(filter);

    }
}
