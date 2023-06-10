using Core.Entities;
using Core.Entities.Concrete.RequestFeatures;
using Core.Utilities.Results.Abstract;
using Entities.Abstract;

namespace Core.Business
{
    public interface IBusinessRepository<TDto,TDtoForManipulation,TRequestParameters>
        where TDto : class,IDto,new()
        where TDtoForManipulation : class, IDto, new()
        where TRequestParameters : RequestParameters, new()
    {
        IDataResult<int> Add(TDtoForManipulation addedItem);
        IResult Update(int id, TDtoForManipulation updatedItem);
        IResult Delete(int id);
        IDataResult<TDto> GetById(int id);
        (IDataResult<IEnumerable<TDto>> result, MetaData metaData) GetAll(TRequestParameters requestParameters);


    }
}
