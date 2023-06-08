using Core.Entities;
using Core.Utilities.Results.Abstract;
using Entities.Abstract;

namespace Core.Business
{
    public interface IBusinessRepository<TDto,TDtoForManipulation>
        where TDto : class,IDto,new()
        where TDtoForManipulation : class, IDto, new()
    {
        IDataResult<int> Add(TDtoForManipulation addedItem);
        IResult Update(int id, TDtoForManipulation updatedItem);
        IResult Delete(int id);
        IDataResult<TDto> GetById(int id);
        IDataResult<IEnumerable<TDto>> GetAll();


    }
}
