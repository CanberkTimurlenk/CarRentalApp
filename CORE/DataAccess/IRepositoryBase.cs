using System.Linq.Expressions;
using Core.Entities.Abstract;
using Core.Entities.Concrete.RequestFeatures;
using Entities.Concrete;

namespace Core.DataAccess
{
    public interface IRepositoryBase<TEntity>
            where TEntity : class, IEntity, new()

    {
        PagedList<TEntity> GetAll(RequestParameters requestParameters, bool trackChanges);
        PagedList<TEntity> GetAllByCondition(Expression<Func<TEntity, bool>> filter, RequestParameters requestParameters, bool trackChanges);
        IEnumerable<TEntity> GetAllAsEnumerable(bool trackChanges);
        IEnumerable<TEntity> GetAllByConditionAsEnumerable(Expression<Func<TEntity, bool>> filter, bool trackChanges);
        TEntity Get(Expression<Func<TEntity, bool>> filter, bool trackChanges);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);

    }
}
