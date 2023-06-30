using Core.Entities;
using Core.Entities.Abstract;
using Core.Entities.Concrete.RequestFeatures;
using Core.Utilities.Results.Abstract;

namespace Core.Business
{
    public interface IBusinessRepository<TDto, TDtoForManipulation, TRequestParameters>
        where TDto : class, IDto, new()
        where TDtoForManipulation : class, IDto, new()
        where TRequestParameters : RequestParameters, new()
    {
        Task<IDataResult<int>> AddAsync(TDtoForManipulation addedItem);
        Task<IResult> UpdateAsync(int id, TDtoForManipulation updatedItem, bool trackChanges);
        Task<IResult> DeleteAsync(int id, bool trackChanges);
        Task<IDataResult<TDto>> GetByIdAsync(int id, bool trackChanges);
        Task<(IDataResult<IEnumerable<TDto>> result, MetaData metaData)> GetAllAsync(TRequestParameters requestParameters, bool trackChanges);


    }
}
