using Core.Business;
using Entities.Concrete.DTOs;
using Core.Utilities.Results.Abstract;
using Entities.Concrete.DTOs.Car;
using Entities.Concrete.RequestFeatures;
using Core.Entities.Concrete.RequestFeatures;

namespace Business.Abstract
{
    public interface ICarService : IBusinessRepository<CarDto, CarForManipulationDto, CarParameters>
    {
        Task<(IDataResult<IEnumerable<CarDto>> result, MetaData metaData)> GetCarsByColorIdAsync(int colorId, CarParameters carParameters, bool trackChanges);
        Task<(IDataResult<IEnumerable<CarDto>> result, MetaData metaData)> GetCarsByBrandIdAsync(int brandId, CarParameters carParameters, bool trackChanges);
        Task<(IDataResult<IEnumerable<CarDetailDto>> result, MetaData metaData)> GetAllCarDetailsAsync(CarParameters carParameters, bool trackChanges);
    }

}
