using System.Linq.Expressions;
using Core.Entities.Concrete.RequestFeatures;
using Entities.Abstract;
using Entities.Concrete;

namespace Core.DataAccess
{
    public interface IRepositoryBase<TEntity, TRequestParameters>
            where TEntity : class, IEntity, new()
            where TRequestParameters : RequestParameters, new()
    {
        PagedList<TEntity> GetAll(TRequestParameters requestParameters, bool trackChanges);
        PagedList<TEntity> GetAllByCondition(Expression<Func<TEntity, bool>> filter, TRequestParameters requestParameters, bool trackChanges);
        IEnumerable<TEntity> GetAllAsEnumerable(bool trackChanges);
        IEnumerable<TEntity> GetAllByConditionAsEnumerable(Expression<Func<TEntity, bool>> filter, bool trackChanges);
        TEntity Get(Expression<Func<TEntity, bool>> filter, bool trackChanges);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);

    }
}
