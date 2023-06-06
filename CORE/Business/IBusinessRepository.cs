using Core.Utilities.Results.Abstract;
using Entities.Abstract;

namespace Core.Business
{
    public interface IBusinessRepository<TEntity>
        where TEntity : class,IEntity,new()
    {
        IResult Add(TEntity addedItem);
        IResult Update(TEntity updatedItem);
        IResult Delete(TEntity deletedItem);
        IDataResult<TEntity> GetById(int id);
        IDataResult<IEnumerable<TEntity>> GetAll();


    }
}
