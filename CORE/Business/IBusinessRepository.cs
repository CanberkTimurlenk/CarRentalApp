using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Core.Utilities.Results.Abstract;

namespace Core.Business
{
    public interface IBusinessRepository<TEntity>
        where TEntity : class,IEntity,new()
    {
        IResult Add(TEntity addedItem);
        IResult Update(TEntity updatedItem);
        IResult Delete(TEntity deletedItem);
        IDataResult<TEntity> GetById(int id);
        IDataResult<List<TEntity>> GetAll();


    }
}
