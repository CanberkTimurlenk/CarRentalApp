using System.Linq.Expressions;
using Core.Entities.Abstract;
using Core.Entities.Concrete.RequestFeatures;
using Entities.Concrete;

namespace Core.DataAccess
{
    public interface IRepositoryBase<TEntity>
            where TEntity : class, IEntity, new()

    {
        Task<PagedList<TEntity>> GetAllAsync(RequestParameters requestParameters, bool trackChanges);
        Task<PagedList<TEntity>> GetAllByConditionAsync(Expression<Func<TEntity, bool>> filter, RequestParameters requestParameters, bool trackChanges);
        Task<IEnumerable<TEntity>> GetAllAsEnumerableAsync(bool trackChanges);
        Task<IEnumerable<TEntity>> GetAllByConditionAsEnumerableAsync(Expression<Func<TEntity, bool>> filter, bool trackChanges);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter, bool trackChanges);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);

    }
}
