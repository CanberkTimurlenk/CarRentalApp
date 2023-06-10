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
        PagedList<TEntity> GetAll(TRequestParameters requestParameters,Expression<Func<TEntity, bool>> filter = null );        
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null);
        TEntity Get(Expression<Func<TEntity, bool>> filter);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);

    }
}
